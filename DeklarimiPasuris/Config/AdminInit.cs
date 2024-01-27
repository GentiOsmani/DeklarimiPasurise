using DeklarimiPasuris.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DeklarimiPasuris.Entities;

namespace DeklarimiPasuris.Config;

public class AdminInit(AppDbContext context, IConfiguration configuration)
{
    private readonly AppDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task Initialize()
    {
        var roles = new List<string> { "Admin", "User" };
        foreach (var role in roles)
            if (!_context.Roles.Any(r => r.Name == role))
                await new RoleStore<IdentityRole>(_context).CreateAsync(new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
        await _context.SaveChangesAsync();

        var admin = new User
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = "deklarimi@admin.com",
            NormalizedEmail = "DEKLARIMI@ADMIN.COM",
            EmailConfirmed = true,
            UserName = "Deklarimi",
            NormalizedUserName = "DEKLARIMI",
            IdentityNumber = "0000000001",
            PhoneNumber = "123123123",
        };
        var userstore = new UserStore<User>(_context);

        if (!_context.AppUser.Any(u => u.UserName == admin.UserName))
        {
            var password = new PasswordHasher<User>().HashPassword(admin, "ImE!1234");
            admin.PasswordHash = password;
            await userstore.CreateAsync(admin);
            await userstore.AddToRoleAsync(admin, roles[0]);
        }
        await _context.SaveChangesAsync();
    }
}
