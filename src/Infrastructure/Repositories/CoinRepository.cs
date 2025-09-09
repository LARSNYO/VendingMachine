using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistance;
using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly AppDbContext _context;
    public CoinRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coin>> GetCoinsAsync()
    {
        return await _context.Coins.OrderByDescending(c => c.Denomination).ToListAsync();
    }

    public async Task<Coin?> GetCoinByIdAsync(Guid id)
    {
        return await _context.Coins.FindAsync(id);
    }

    public async Task<Coin?> GetByDenominationAsync(int denomination)
    {
        return await _context.Coins.FirstOrDefaultAsync(c => c.Denomination == denomination);
    }

    public async Task AddCoinAsync(Coin coin)
    {
        await _context.Coins.AddAsync(coin);
    }

    public void UpdateCoin(Coin coin)
    {
        _context.Coins.Update(coin);
    }

    public async Task DeleteCoinAsync(Guid id)
    {
        var coin = await GetCoinByIdAsync(id);
        if (coin != null)
        {
            _context.Coins.Remove(coin);
        }
    }

    public async Task SaveCoinChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CoinExistAsync(Guid id)
    {
        return await _context.Coins.AnyAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Coin>> GetAvailableCoinsAsync()
    {
        return await _context.Coins.Where(c => c.Quantity > 0).ToListAsync();
    }
}
