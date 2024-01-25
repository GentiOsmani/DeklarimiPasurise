namespace DeklarimiPasuris.Models;

public class FinancialObligationModel
{
    public string? Kreditore { get; set; }
    public string? Debitore { get; set; }
    public string? Reason { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Duration { get; set; }
    public double? StartedValue { get; set; }
    public double? LeftValue { get; set; }
    public double? TotalValue { get; set; }
    public string? Owned { get; set; }
}