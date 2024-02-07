using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class Bond : BaseEntity
{
    public string? UserId { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public DateTime? Date { get; set; }

    public string? Origine { get; set; }

    public string? OwnedBy { get; set; }


    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}