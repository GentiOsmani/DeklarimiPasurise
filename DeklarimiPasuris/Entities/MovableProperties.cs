using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class MovableProperties : BaseEntity
{
    public string? UserId { get; set; }

    public string? Type { get; set; }

    public string? Origine { get; set; }

    public DateTime? DateOwned { get; set; }

    public string? UniquePart { get; set; }
    
    public double? Price { get; set; }
    
    public string? OwnedBy { get; set; }


    // Navigation properties

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
