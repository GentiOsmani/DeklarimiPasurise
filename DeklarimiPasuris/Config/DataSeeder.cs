using DeklarimiPasuris.Data;
using DeklarimiPasuris.Entities;

namespace DeklarimiPasuris.Config;

public class DataSeeder(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task Seed()
    {
        await _context.AddAsync(new Municipality
        {
            Name = "Prishtina",
        });
        await _context.SaveChangesAsync();
    }
}
