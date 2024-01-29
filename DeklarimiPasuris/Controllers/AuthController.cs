using DeklarimiPasuris.Data;
using DeklarimiPasuris.Entities;
using DeklarimiPasuris.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeklarimiPasuris.Controllers;

public class AuthController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context, IWebHostEnvironment environment) : Controller
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly AppDbContext _context = context;
    private readonly IWebHostEnvironment _environment = environment;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return Ok(model);
        }
        var user = await _userManager.Users.Where(x => x.IdentityNumber == model.IdentityNumber).FirstOrDefaultAsync();

        var response = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

        if (!response.Succeeded)
        {
            ModelState.AddModelError("Password", "Kredencialet e gabuara");
            return View(model);
        }
        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        HttpContext.Session.Clear();
        return Content("<script>window.location = '/Auth/Login';</script>", "text/html");
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        ViewBag.Munis = await _context.Municipalities.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Munis = await _context.Municipalities.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToListAsync();
            return View(model);
        }

        var build = new User
        {
            Email = model.Email,
            EmailConfirmed = true,
            NormalizedEmail = model.Email.ToUpper(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            NormalizedUserName = model.Email.ToUpper(),
            MiddleName = model.MiddleName,
            IdentityNumber = model.IdentityNumber,
            MunicipalityId = model.Municipality,
            Image = AddImage(model.Image)
        };

        var create = await _userManager.CreateAsync(build, model.Password);

        if (create.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(model);

    }

    private string AddImage(IFormFile img)
    {
        if (img is null)
        {
            return "";
        }

        string fileName;
        // iterate for every file in the list and save them

        // Create an unique name for every file so you do not have 'File name collision';
        // The prefix guid is a good way to make sure u have unique files stored.
        fileName = $"{Guid.NewGuid()}_{img.FileName}";

        // the path is where the file will be stored
        string path = Path.Combine(_environment.WebRootPath, "img", fileName);

        using (var fileStream = new FileStream(path, FileMode.Create))
            img.CopyTo(fileStream);

        return fileName;
    }
}