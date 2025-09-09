using Application.DTOs.Drink;

namespace Application.Interfaces.Services;

public interface IDrinkService
{
    Task<IEnumerable<DrinkGetResponseDto>> GetDrinksAsync();
    Task<DrinkGetResponseDto?> GetDrinkByIdAsyncAsNoTracking(Guid id);
    Task<DrinkGetResponseDto?> GetDrinkWithBrandByIdAsync(Guid id);
    Task<DrinkGetResponseDto> CreateDrinkAsync(DrinkPostRequestDto dto);
    Task<DrinkGetResponseDto?> UpdateDrinkAsync(Guid id, DrinkPostRequestDto dto);
    Task<bool> DeleteDrinkAsync(Guid id);
    Task<bool> DrinkExistAsync(Guid id);
    Task<IEnumerable<DrinkGetResponseDto>> GetFilterAsync(Guid? brandId, int? minPrice);
}
