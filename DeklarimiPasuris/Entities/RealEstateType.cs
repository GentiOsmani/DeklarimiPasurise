using DeklarimiPasuris.Entities.Common;

namespace DeklarimiPasuris.Entities;

public class RealEstateType : BaseEntity
{
    public string? Name { get; set; }

    public ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
