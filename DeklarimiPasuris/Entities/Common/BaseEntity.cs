using System.ComponentModel.DataAnnotations;

namespace DeklarimiPasuris.Entities.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }
}
