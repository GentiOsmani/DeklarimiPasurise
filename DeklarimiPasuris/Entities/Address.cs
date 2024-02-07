using DeklarimiPasuris.Entities.Common;

namespace DeklarimiPasuris.Entities;

public sealed class Address : BaseEntity
{
    public string? Name { get; set; }


    public ICollection<Institutions> Institutions { get; set; } = new List<Institutions>();
}
