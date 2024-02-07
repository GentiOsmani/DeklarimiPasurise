using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class RealEstate : BaseEntity
{
    public string? UserId { get; set; }

    public int? RealEstateTypeId { get; set; }

    public string? Origine { get; set; }
    
    public double? Area { get; set; }
    
    public DateTime? DateOwned { get; set; }
    
    public double? Price { get; set; }
    
    public string? OwnedBy { get; set; }


    // Navigation properties

    [ForeignKey(nameof(RealEstateTypeId))]
    public RealEstateType? RealEstateType { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
