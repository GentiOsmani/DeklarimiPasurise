using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class Action : BaseEntity
{
    public string? UserId { get; set; }

    public string? Type { get; set; }
    
    public string? Name { get; set; }
    
    public string? OwnedBy { get; set; }
    
    public double? Percent { get; set; }
    
    public DateTime? DateRegistred { get; set; }
    
    public DateTime? DateOwned { get; set; }
    
    public double? Price { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
