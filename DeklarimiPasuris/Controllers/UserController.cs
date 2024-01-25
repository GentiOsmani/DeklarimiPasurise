using DeklarimiPasuris.Data;
using DeklarimiPasuris.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeklarimiPasuris.Controllers;

public class UserController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var users = await _context.AppUser.ToListAsync();
        return View(users);
    }


    public async Task<IActionResult> Details(string userId)
    {
        var user = await _context.AppUser.FirstOrDefaultAsync(x => x.Id == userId);
        return View(user);
    }

    public async Task<IActionResult> Edit(User model)
    {
        // edit the user
        return View();
    }
}
