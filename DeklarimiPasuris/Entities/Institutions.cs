using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class Institutions : BaseEntity
{
    public string? Name { get; set; }

    public int? AddressId { get; set; }


    [ForeignKey(nameof(AddressId))]
    public Address? Address { get; set; }
}
