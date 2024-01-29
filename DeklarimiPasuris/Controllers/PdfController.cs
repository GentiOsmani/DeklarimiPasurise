﻿using Microsoft.AspNetCore.Mvc;
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
        var viewContent = @"
            @model DeclarationModel

<div class='card p-4'>
    <h3 class='text-primary fw-bold p-3 rounded' style='background-color: #d8ebff'>Deklarimi i Pasurisë së @ViewBag.User</h3>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Pasuritë e Paluajtshme</h5>
    </dv>
    <table class='table'>
        < thead>
            <tr>
                <th>Pronësia</th>
                <th>Lloji</th>
                <th>Burimi</th>
                <th>Vendi</th>
                <th>Banka</th>
                <th>Shuma</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.IncomeModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.IncomeModel?.Owned</td>
                <td>@Model.IncomeModel?.IncomeType</td>
                <td>@Model.IncomeModel?.Source</td>
                <td>@Model.IncomeModel?.Country</td>
                <td>@Model.IncomeModel?.Bank</td>
                <td>@Model.IncomeModel?.Amount</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Pasuritë e luajtshme mbi (3000.00) euro</h5>
    </dv>
    <table class='table'>
        <thead>
            <tr>
                <th>Pronësia</th>
                <th>Lloji</th>
                <th>Sipërfaqja</th>
                <th>Vlera</th>
                <th>Përfitimi</th>
                <th>Data </th>
                <th>Prejardhja</th>
                <th>Të Veqanta</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.MovableAssetsModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.MovableAssetsModel?.Owned</td>
                <td>@Model.MovableAssetsModel?.Type</td>
                <td>@Model.MovableAssetsModel?.Area</td>
                <td>@Model.MovableAssetsModel?.Price</td>
                <td>@Model.MovableAssetsModel?.Origine</td>
                <td>@Model.MovableAssetsModel?.Date</td>
                <td>@Model.MovableAssetsModel?.Origine</td>
                <td>@Model.MovableAssetsModel?.Unique</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2' >
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Zotrimi i aksioneve në shoqëri tregtare</h5>
    </dv>
    <table class='table'>
        <thead>
            <tr>
                <th>Emërtimi</th>
                <th>Lloji</th>
                <th>Aksionet</th>
                <th>Vlera</th>
                <th>Përqindja</th>
                <th>Data e Regjistrimit </th>
                <th>Data e Blerjes</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.ActionsModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.ActionsModel?.Name</td>
                <td>@Model.ActionsModel?.Type</td>
                <td>@Model.ActionsModel?.Actions</td>
                <td>@Model.ActionsModel?.Value</td>
                <td>@Model.ActionsModel?.Percent</td>
                <td>@Model.ActionsModel?.DateRegistred</td>
                <td>@Model.ActionsModel?.DateOwned</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3' > Posedimi i letrave me vlerë</h5>
    </dv>
    <table class='table'>
        <thead>
            <tr>
                <th>Emërtimi</th>
                <th>Vlera</th>
                <th>Prejardhja</th>
                <th>Data</th>
                <th>Bonds</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.BondModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.BondModel?.Name</td>
                <td>@Model.BondModel?.ValueNow</td>
                <td>@Model.BondModel?.Origin</td>
                <td>@Model.BondModel?.Date</td>
                <td>@Model.BondModel?.Bond</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Posedimi i kriptovalutave</h5>
    </dv>
    <table class='table'>
        <thead>
            <tr>
                <th>Emërtimi</th>
                <th>Vlera tani</th>
                <th>Vlera e blerë</th>
                <th>Sasia</th>
                <th>Data</th>
                <th>Crypto </th>
                <th>Prejardhja</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.CryptoModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.CryptoModel?.Name</td>
                <td>@Model.CryptoModel?.ValueNow</td>
                <td>@Model.CryptoModel?.ValueBought</td>
                <td>@Model.CryptoModel?.Quantity</td>
                <td>@Model.CryptoModel?.Date</td>
                <td>@Model.CryptoModel?.Crypto</td>
                <td>@Model.CryptoModel?.Origine</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Para të gatshme në llogari rrjedhëse</h5>
    </dv>
    <table class='table'>
        <thead>
            <tr>
                <th>Lloji</th>
                <th>Institutcioni</th>
                <th>Vendi</th>
                <th>Prejardhja</th>
                <th>Lloji i Llogarisë</th>
                <th>Pronësia</th>
                <th>Vlera</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.LiquidMoneyModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.LiquidMoneyModel?.Type</td>
                <td>@Model.LiquidMoneyModel?.Institution</td>
                <td>@Model.LiquidMoneyModel?.Country</td>
                <td>@Model.LiquidMoneyModel?.Origine</td>
                <td>@Model.LiquidMoneyModel?.AccountType</td>
                <td>@Model.LiquidMoneyModel?.Owned</td>
                <td>@Model.LiquidMoneyModel?.Value</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Të drejtat dhe detyrimet financiare ndaj personave juridik ose fizik</h5>
    </dv>
    <table class='table' >
        <thead>
            <tr>
                <th>Kredit</th>
                <th>Debit</th>
                <th>Arsyeja</th>
                <th>Data e Fillimit</th>
                <th>Data e Mbarimit</th>
                <th>Kohëzgjatja (në muaj)</th>
                <th>Vlera në fillim</th>
                <th>Vlera në mbetur</th>
                <th>Vlera në totale</th>
                <th>Pronësia</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.FinancialObligationModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.FinancialObligationModel?.Kreditore</td>
                <td>@Model.FinancialObligationModel?.Debitore</td>
                <td>@Model.FinancialObligationModel?.Reason</td>
                <td>@Model.FinancialObligationModel?.StartDate</td>
                <td>@Model.FinancialObligationModel?.EndDate</td>
                <td>@Model.FinancialObligationModel?.Duration</td>
                <td>@Model.FinancialObligationModel?.StartedValue</td>
                <td>@Model.FinancialObligationModel?.LeftValue</td>
                <td>@Model.FinancialObligationModel?.TotalValue</td>
                <td>@Model.FinancialObligationModel?.Owned</td>
            </tr>
        </tbody>
    </table>
</div>
<div class='card p-2'>
    <dv>
        <h5 class='text-center text-dark text-capitalize fw-bold mt-3'>Donacionet dhe shpenzimet</h5>
    </dv>
    <table class='table'>
        <thead>
            <tr>
                <th>Lloji</th>
                <th>Vlera</th>
                <th>Emërtimi</th>
                <th>Data</th>
                <th>Qëllimi</th>
                <th>Pronësia</th>
            </tr>
        </thead>
        <tbody>
            @if (Model.DonationModel is null)
            {
                <tr>
                    <td>Nuk ka të dhëna</td>
                </tr>
            }
            <tr>
                <td>@Model.DonationModel?.Type</td>
                <td>@Model.DonationModel?.Value</td>
                <td>@Model.DonationModel?.Naming</td>
                <td>@Model.DonationModel?.Date</td>
                <td>@Model.DonationModel?.Reason</td>
                <td>@Model.DonationModel?.Owned</td>
            </tr>
        </tbody>
    </table>
</div>

        "; // Replace this with your actual Razor view content
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