using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistance;
using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;
public class DrinkRepository : IDrinkRepository
{
        private readonly AppDbContext _context;
    public DrinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Drink>> GetDrinksAsync()
    {
        return await _context.Drinks.Include(d => d.Brand).ToListAsync();
    }

    public async Task<Drink?> GetDrinkByIdAsyncAsNoTracking(Guid id)
    {
        return await _context.Drinks.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Drink?> GetDrinkWithBrandByIdAsync(Guid id)
    {
        return await _context.Drinks.Include(d => d.Brand).FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddDrinkAsync(Drink drink)
    {
        await _context.Drinks.AddAsync(drink);
    }

    public void UpdateDrink(Drink drink)
    {
        _context.Drinks.Update(drink);
    }

    public async Task DeleteDrinkAsync(Guid id)
    {
        var drink = await GetDrinkByIdAsyncAsNoTracking(id);
        if (drink != null)
        {
            _context.Drinks.Remove(drink);
        }
    }

    public async Task SaveDrinkChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DrinkExistAsync(Guid id)
    {
        return await _context.Drinks.AnyAsync(d => d.Id == id);
    }
}
