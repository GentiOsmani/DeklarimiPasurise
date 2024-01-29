using DeklarimiPasuris.Data;
using DeklarimiPasuris.Entities;
using DeklarimiPasuris.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DeklarimiPasuris.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _environment = environment;
            _emailSender = emailSender;
        }

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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");
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
                var receiver = model.Email;
                var subject = "Regjistrimi në SAPK";
                var message = "Regjistrimi juaj ne SAPK eshte kryer me sukses.\n\nManuali per Regjistrim:\nNumri Personal: Numri juaj Personal\nFjalëkalimi: " + model.Password ;

                await _emailSender.SendEmailAsync(receiver, subject, message);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
            return View();
        }

        private string AddImage(IFormFile img)
        {
            if (img is null)
            {
                return "";
            }

            string fileName = $"{Guid.NewGuid()}_{img.FileName}";
            string path = Path.Combine(_environment.WebRootPath, "img", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
                img.CopyTo(fileStream);

            return fileName;
        }
    }
}
