using Application.DTOs.Brand;

namespace Application.Interfaces.Services;

public interface IBrandService
{
    Task<IEnumerable<BrandGetResponseDto>> GetBrandsAsync();
    Task<BrandGetResponseDto?> GetBrandByIdAsyncAsNoTracking(Guid id);
    Task<BrandGetResponseDto> CreateBrandAsync(BrandPostRequestDto dto);
    Task<BrandGetResponseDto?> UpdateBrandAsync(Guid id, BrandPostRequestDto dto);
    Task<bool> DeleteBrandAsync(Guid id);
    Task<bool> BrandExistAsync(Guid id);
}
