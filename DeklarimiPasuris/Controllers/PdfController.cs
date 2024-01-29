using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using DeklarimiPasuris.Models;

public class PdfController : Controller
{
    private readonly ViewRenderer _viewRenderer;
    private readonly IConverter _pdfConverter;

    public PdfController(ViewRenderer viewRenderer, IConverter pdfConverter)
    {
        _viewRenderer = viewRenderer;
        _pdfConverter = pdfConverter;
    }

    public async Task<IActionResult> GeneratePdf()
    {
        var model = new DeclarationModel();
        var viewContent = "";
        var result = await _viewRenderer.RenderViewToStringAsync(viewContent, model);

        var pdf = _pdfConverter.Convert(new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                PaperSize = PaperKind.A4,
            },
            Objects = {
                new ObjectSettings() {
                    HtmlContent = result,
                }
            }
        });

        // Return the generated PDF as a file for download
        return File(pdf, "application/pdf", $"output_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf");
    }
}