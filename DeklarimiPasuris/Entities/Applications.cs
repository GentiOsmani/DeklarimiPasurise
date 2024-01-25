using DeklarimiPasuris.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeklarimiPasuris.Entities;

public sealed class Applications : BaseEntity
{
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
