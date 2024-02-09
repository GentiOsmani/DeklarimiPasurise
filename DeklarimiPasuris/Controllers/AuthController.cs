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
                return View(model); // Return the view with validation errors
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.IdentityNumber == model.IdentityNumber);

            if (user == null)
            {
                ModelState.AddModelError("IdentityNumber", "ID-ja e Dhënë Nuk Ggziston.");
                return View(model); // Return the view with error under the ID field
            }

            var response = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (!response.Succeeded)
            {
                ModelState.AddModelError("Password", "Passwordi është gabim.");
                return View(model); // Return the view with error under the password field
            }

            return RedirectToAction("Index", "Home"); // Redirect to the home page upon successful login
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
                var subject = "Regjistrimi në APK";
                var message = "Regjistrimi juaj ne APK eshte kryer me sukses.\n\nManuali per Regjistrim:\nQasuni përmes Numrit tuaj Personal\nDhe përmes Fjalëkalimit: '" + model.Password + "'";

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