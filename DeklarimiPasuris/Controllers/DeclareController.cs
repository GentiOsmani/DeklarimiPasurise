using DeklarimiPasuris.Data;
using DeklarimiPasuris.Entities;
using DeklarimiPasuris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeklarimiPasuris.Controllers;

public class DeclareController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var applications = await _context.Applications
            .Include(x => x.User)
            .ToListAsync();
        return View(applications);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string userId, string date)
    {
        var user = await _context.AppUser.FirstOrDefaultAsync(x => x.Id == userId);
        ViewBag.User = $"{user.FirstName} {user.LastName}";

        var pareseDate = DateTime.Parse(date);

        var incomes = await _context.Incomes
            .Where(x => x.UserId == userId)
            .Select(x => new IncomeModel
            {
                IncomeType = x.Type,
                Source = x.Origine,
                Amount = x.Price,
                Bank = x.Bank,
                Country = x.Place,
                Owned = x.OwnedBy,
            })
            .FirstOrDefaultAsync();

        var actions = await _context.Actions
            .Where(x => x.UserId == userId )
            .Select(x => new ActionsModel
            {
                DateOwned = x.DateOwned,
                Type = x.Type,
                DateRegistred = x.DateRegistred,
                Name = x.Name,
                Percent = x.Percent,
                Value = x.Price,
            })
            .FirstOrDefaultAsync();

        var realEstate = await _context.RealEstates
            .Where(x => x.UserId == userId )
            .Select(x => new RealEstateModel
            {
                Area = x.Area,
                Date = x.DateOwned,
                Origine = x.Origine,
                Erned = x.OwnedBy,
                Owned = x.OwnedBy,
                Price = x.Price,
            })
            .FirstOrDefaultAsync();

        var movable = await _context.MovableProperties
            .Where(x => x.UserId == userId )
            .Select(x => new MovableAssetsModel
            {
                Type = x.Type,
                Date = x.DateOwned,
                Origine = x.Origine,
                Erned = x.OwnedBy,
                Owned = x.OwnedBy,
                Price = x.Price,
                Unique = x.UniquePart
            })
            .FirstOrDefaultAsync();

        var bonds = await _context.Bonds
            .Where(x => x.UserId == userId )
            .Select(x => new BondModel
            {
                Name = x.Name,
                ValueNow = x.Price,
                Date = x.Date,
                Origin = x.Origine,
            })
            .FirstOrDefaultAsync();

        var crypto = await _context.Cryptocurrencies
            .Where(x => x.UserId == userId )
            .Select(x => new CryptoModel
            {
                Date = x.Date,
                Name = x.Name,
                Origine = x.Origine,
                Quantity = x.Quantity,
                ValueNow = x.ActualMoney
            })
            .FirstOrDefaultAsync();

        var liquid = await _context.LiquidMoney
            .Where(x => x.UserId == userId )
            .Select(x => new LiquidMoneyModel
            {
                AccountType = x.AssetType,
                Country = x.Place,
                Institution = x.InstitutionName,
                Origine = x.Origine,
                Owned = x.Origine,
                Type = x.Type,
                Value = x.Price
            })
            .FirstOrDefaultAsync();

        var financial = await _context.FinancialObligations
            .Where(x => x.UserId == userId )
            .Select(x => new FinancialObligationModel
            {
                Debitore = x.Debit,
                Kreditore = x.Credit,
                Duration = x.Duration,
                StartDate = x.Date,
                EndDate = x.DateClosed,
                LeftValue = x.LeftValue,
                StartedValue = x.StartedValue,
                TotalValue = x.TotalValue,
                Reason = x.FinancialReason,
                Owned = x.OwnedBy
            })
            .FirstOrDefaultAsync();

        var donation = await _context.Donations
            .Where(x => x.UserId == userId )
            .Select(x => new DonationModel
            {
                Date = x.Date,
                Value = x.Price,
                Naming = x.Description,
                PoliticalSubject = x.PoliticalSubject,
                Type = x.Type,
                Reason = x.Reason
            })
            .FirstOrDefaultAsync();

        var model = new DeclarationModel
        {
            IncomeModel = incomes,
            FinancialObligationModel = financial,
            ActionsModel = actions,
            BondModel = bonds,
            CryptoModel = crypto,
            DonationModel = donation,
            LiquidMoneyModel = liquid,
            MovableAssetsModel = movable,
            RealEstateModel = realEstate,
        };

        return View(model);
    }

    #region Create

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(DeclarationModel model)
    {
        var user = await _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync();

        var date = DateTime.Now;

        if (model.IncomeModel is not null)
        {
            await _context.AddAsync(new Incomes
            {
                Type = model.IncomeModel.IncomeType,
                Bank = model.IncomeModel.Bank,
                Created = date,
                Modified = date,
                Origine = model.IncomeModel.Source,
                Place = model.IncomeModel.Country,
                Price = model.IncomeModel.Amount,
                UserId = user.Id
            });
        }

        if (model.ActionsModel is not null)
        {
            await _context.AddAsync(new Entities.Action
            {
                Type = model.ActionsModel.Type,
                Name = model.ActionsModel.Name,
                DateOwned = model.ActionsModel.DateOwned,
                DateRegistred = model.ActionsModel.DateRegistred,
                Percent = model.ActionsModel.Percent,
                Price = model.ActionsModel.Value,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.RealEstateModel is not null)
        {
            await _context.AddAsync(new RealEstate
            {
                Area = model.RealEstateModel.Area,
                Origine = model.RealEstateModel.Origine,
                OwnedBy = model.RealEstateModel.Owned,
                Price = model.RealEstateModel.Price,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.MovableAssetsModel is not null)
        {
            await _context.AddAsync(new MovableProperties
            {
                DateOwned = model.MovableAssetsModel.Date,
                Origine = model.MovableAssetsModel?.Origine,
                Price = model.MovableAssetsModel?.Price,
                UniquePart = model.MovableAssetsModel?.Unique,
                OwnedBy = model.MovableAssetsModel?.Owned,
                Type = model.MovableAssetsModel?.Type,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.BondModel is not null)
        {
            await _context.AddAsync(new Bond
            {
                Name = model.BondModel.Name,
                Price = model.BondModel.ValueNow,
                Date = model.BondModel.Date,
                Origine = model.BondModel.Origin,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.CryptoModel is not null)
        {
            await _context.AddAsync(new Cryptocurrency
            {
                Name = model.CryptoModel.Name,
                ActualMoney = model.CryptoModel.ValueNow,
                Date = model.CryptoModel?.Date,
                Origine = model.CryptoModel.Origine,
                Quantity = model.CryptoModel.Quantity,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.LiquidMoneyModel is not null)
        {
            await _context.AddAsync(new LiquidMoney
            {
                Type = model.LiquidMoneyModel.Type,
                AssetType = model.LiquidMoneyModel.AccountType,
                InstitutionName = model.LiquidMoneyModel.Institution,
                Origine = model.LiquidMoneyModel.Origine,
                Place = model.LiquidMoneyModel.Country,
                Price = model.LiquidMoneyModel.Value,
                OwnedBy = model.LiquidMoneyModel.Owned,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.FinancialObligationModel is not null)
        {
            await _context.AddAsync(new FinancialObligation
            {
                Credit = model.FinancialObligationModel.Kreditore,
                Debit = model.FinancialObligationModel.Debitore,
                Date = model.FinancialObligationModel.StartDate,
                StartedValue = model.FinancialObligationModel.StartedValue,
                TotalValue = model.FinancialObligationModel.TotalValue,
                LeftValue = model.FinancialObligationModel.LeftValue,
                DateClosed = model.FinancialObligationModel.EndDate,
                Duration = model.FinancialObligationModel.Duration,
                FinancialReason = model.FinancialObligationModel.Reason,
                OwnedBy = model.FinancialObligationModel.Owned,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        if (model.DonationModel is not null)
        {
            await _context.AddAsync(new Donation
            {
                Type = model.DonationModel.Type,
                Date = model.DonationModel.Date,
                OwnedBy = model.DonationModel.Owned,
                Price = model.DonationModel.Value,
                Reason = model.DonationModel.Reason,
                Description = model.DonationModel.Naming,
                Created = date,
                Modified = date,
                UserId = user.Id
            });
        }

        var saved = await _context.SaveChangesAsync();

        if (saved > 0)
        {
            await _context.AddAsync(new Applications
            {
                UserId = user.Id,
                Date = date,
                Created = date,
                Modified = date,
            });

            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Home");
    }

    #endregion

}













