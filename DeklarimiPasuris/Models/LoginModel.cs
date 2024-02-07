using System.ComponentModel.DataAnnotations;

namespace DeklarimiPasuris.Models;

public class LoginModel
{
    [Required]
    [MaxLength(10)]
    public string? IdentityNumber { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
