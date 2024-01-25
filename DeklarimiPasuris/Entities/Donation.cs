using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class Donation : BaseEntity
{
    public string? UserId { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public string? PoliticalSubject { get; set; }

    public DateTime? Date { get; set; }

    public double? Price { get; set; }

    public string? OwnedBy { get; set; }

    public string? Reason { get; set; }

    public string? DonationSource { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
