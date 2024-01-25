using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DeklarimiPasuris.Entities;

public sealed class User : IdentityUser
{
    // Scalar properties

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }
    
    public string? Image { get; set; }
    
    public string? IdentityNumber { get; set; }
    
    public int? MunicipalityId { get; set; }
    
    public string? Function { get; set; }
    
    public DateTime? Created { get; set; }



    // Navigation properties

    [ForeignKey(nameof(MunicipalityId))]
    public Municipality? Municipality { get; set; }
    
    public ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();

    public ICollection<MovableProperties> MovableProperties { get; set; } = new List<MovableProperties>();

    public ICollection<LiquidMoney> LiquidMoney { get; set; } = new List<LiquidMoney>();

    public ICollection<Incomes> Incomes { get; set; } = new List<Incomes>();

    public ICollection<FinancialObligation> FinancialObligations { get; set; } = new HashSet<FinancialObligation>();

    public ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public ICollection<Cryptocurrency> Cryptocurrencies { get; set; } = new List<Cryptocurrency>();

    public ICollection<Bond> Bond { get; set; } = new List<Bond>();

    public ICollection<AdditionalFunction> AdditionalFunctions { get; set; } = new List<AdditionalFunction>();

    public ICollection<Action> Action { get; set; } = new List<Action>();

    public ICollection<Applications> Applications { get; set; } = new List<Applications>();
}
