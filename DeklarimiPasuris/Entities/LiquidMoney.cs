using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class LiquidMoney : BaseEntity
{
    public string? UserId { get; set; }

    public string? Type { get; set; }

    public string? InstitutionName { get; set; }
    
    public string? Place { get; set; }

    public string? UniquePart { get; set; }

    public double? Price { get; set; }

    public string? OwnedBy { get; set; }

    public string? Origine { get; set; }

    public string? AssetType { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
