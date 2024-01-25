using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class AdditionalFunction : BaseEntity
{
    public string? UserId { get; set; }

    public int? InstitutionId { get; set; }

    public string? Function { get; set; }

    public int? MunicipalityId { get; set; }

    public string? Position { get; set; }

    public DateTime? DateAppointed { get; set; }

    public DateTime? DateFired { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
