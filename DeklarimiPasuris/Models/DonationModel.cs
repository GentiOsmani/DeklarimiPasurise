namespace DeklarimiPasuris.Models;


public class DonationModel
{
    public string? Type { get; set; }
    public string? PoliticalSubject { get; set; }
    public double? Value { get; set; }
    public string? Naming { get; set; }
    public DateTime? Date { get; set; }
    public string? Reason { get; set; }
    public string? Owned { get; set; }
}