using DeklarimiPasuris.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeklarimiPasuris.Data;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<User> AppUser {  get; set; }

    public DbSet<Entities.Action> Actions { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<AdditionalFunction> AdditionalFunctions { get; set; }

    public DbSet<Bond> Bonds { get; set; }

    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }

    public DbSet<Donation> Donations { get; set; }

    public DbSet<FinancialObligation> FinancialObligations { get; set; }

    public DbSet<Incomes> Incomes { get; set; }

    public DbSet<Institutions> Institutions { get; set; }

    public DbSet<LiquidMoney> LiquidMoney { get; set; }

    public DbSet<MovableProperties> MovableProperties { get; set; }

    public DbSet<Municipality> Municipalities { get; set; }

    public DbSet<RealEstate> RealEstates { get; set; }

    public DbSet<RealEstateType> RealEstateTypes { get; set; }

    public DbSet<Applications> Applications { get; set; }
}
