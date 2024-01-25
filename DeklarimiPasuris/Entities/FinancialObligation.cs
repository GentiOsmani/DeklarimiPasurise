using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class FinancialObligation : BaseEntity
{
    public string? UserId { get; set; }

    public string? Credit { get; set; }

    public string? Debit { get; set; }

    public string? FinancialReason { get; set; }

    public DateTime? Date { get; set; }
                   
    public DateTime? DateClosed { get; set; }

    public int? Duration { get; set; }

    public double? StartedValue { get; set; }

    public double? LeftValue { get; set; }

    public double? TotalValue { get; set; }

    public string? OwnedBy { get; set; }


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
