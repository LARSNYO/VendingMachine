using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetBrandsAsync();
    Task<Brand?> GetBrandByIdAsync(Guid id);
    Task AddBrandAsync(Brand brand);
    void UpdateBrand(Brand brand);
    Task DeleteBrandAsync(Guid id);
    Task SaveBrandChangesAsync();
    Task<bool> BrandExistAsync(Guid id);
}
