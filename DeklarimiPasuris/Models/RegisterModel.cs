using System.ComponentModel.DataAnnotations;

namespace DeklarimiPasuris.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Kjo fushe esht obligative")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Kjo fushe esht obligative")]
    public string? LastName { get; set; }
    
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Kjo fushe esht obligative")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Kjo fushe esht obligative")]
    public string? IdentityNumber { get; set; }
    
    public int Municipality { get; set; }
    
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "Kjo fushe esht obligative")]
    [DataType(DataType.Password)]
    public string? Password {  get; set; }

    [Required(ErrorMessage = "Kjo fushe esht obligative")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? ConfirmPassword {  get; set; }
}
