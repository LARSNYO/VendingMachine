using Domain.Entities;
using Application.DTOs.Brand;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.DTOs.Drink;

namespace Infrastructure.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<BrandGetResponseDto>> GetBrandsAsync()
    {
        var brands = await _brandRepository.GetBrandsAsync();
        return brands.Select(MapBrandToGetDto);
    }

    public async Task<BrandGetResponseDto?> GetBrandByIdAsync(Guid id)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id);
        return brand is null ? null : MapBrandToGetDto(brand);
    }

    public async Task<BrandGetResponseDto> CreateBrandAsync(BrandPostRequestDto dto)
    {
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _brandRepository.AddBrandAsync(brand);
        await _brandRepository.SaveBrandChangesAsync();

        return MapBrandToGetDto(brand);
    }

    public async Task<BrandGetResponseDto?> UpdateBrandAsync(Guid id, BrandPostRequestDto dto)
    {
        var existing = await _brandRepository.GetBrandByIdAsync(id);
        if (existing is null) return null;

        var updated = existing with
        {
            Name = dto.Name
        };

        _brandRepository.UpdateBrand(updated);
        await _brandRepository.SaveBrandChangesAsync();

        return MapBrandToGetDto(updated);
    }

    public async Task<bool> DeleteBrandAsync(Guid id)
    {
        var existing = await _brandRepository.GetBrandByIdAsync(id);
        if (existing is null) return false;

        await _brandRepository.DeleteBrandAsync(id);
        await _brandRepository.SaveBrandChangesAsync();

        return true;
    }

    public async Task<bool> BrandExistAsync(Guid id)
    {
        return await _brandRepository.BrandExistAsync(id);
    }

    private static BrandGetResponseDto MapBrandToGetDto(Brand brand)
    {
        return new BrandGetResponseDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Drinks = brand.Drinks?.Select(d => new DrinkGetResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                Quantity = d.Quantity,
                ImagePath = d.ImagePath,
                BrandId = d.BrandId,
                BrandName = d.Brand?.Name ?? ""
            }).ToList()
        };
    }
}
