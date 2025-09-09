using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistance;
using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly AppDbContext _context;

    public BrandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetBrandByIdAsyncAsNoTracking(Guid id)
    {
        return await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddBrandAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
    }

    public void UpdateBrand(Brand brand)
    {
        _context.Brands.Update(brand);
    }

    public async Task DeleteBrandAsync(Guid id)
    {
        var brand = await GetBrandByIdAsyncAsNoTracking(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
        }
    }

    public async Task SaveBrandChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> BrandExistAsync(Guid id)
    {
        return await _context.Brands.AnyAsync(b => b.Id == id);
    }
}
