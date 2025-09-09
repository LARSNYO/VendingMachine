using Domain.Entities;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.DTOs.Drink;

namespace Infrastructure.Services;

public class DrinkService : IDrinkService
{
    private readonly IDrinkRepository _drinkRepository;
    public DrinkService(IDrinkRepository drinkRepository)
    {
        _drinkRepository = drinkRepository;
    }
    public async Task<IEnumerable<DrinkGetResponseDto>> GetDrinksAsync()
    {
        var drinks = await _drinkRepository.GetDrinksAsync();
        return drinks.Select(MapDrinkToGetDto);
    }

    public async Task<DrinkGetResponseDto?> GetDrinkByIdAsyncAsNoTracking(Guid id)
    {
        var drink = await _drinkRepository.GetDrinkByIdAsyncAsNoTracking(id);
        return drink is null ? null : MapDrinkToGetDto(drink);
    }

    public async Task<DrinkGetResponseDto?> GetDrinkWithBrandByIdAsync(Guid id)
    {
        var drink = await _drinkRepository.GetDrinkWithBrandByIdAsync(id);
        return drink is null ? null : MapDrinkToGetDto(drink);
    }

    public async Task<DrinkGetResponseDto> CreateDrinkAsync(DrinkPostRequestDto dto)
    {
        var drink = new Drink
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            ImagePath = dto.ImagePath,
            BrandId = dto.BrandId
        };

        await _drinkRepository.AddDrinkAsync(drink);
        await _drinkRepository.SaveDrinkChangesAsync();

        return MapDrinkToGetDto(drink);
    }

    public async Task<DrinkGetResponseDto?> UpdateDrinkAsync(Guid id, DrinkPostRequestDto dto)
    {
        var existing = await _drinkRepository.GetDrinkByIdAsyncAsNoTracking(id);
        if (existing is null) return null;

        var updated = existing with
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            ImagePath = dto.ImagePath,
            BrandId = dto.BrandId
        };

        _drinkRepository.UpdateDrink(updated);
        await _drinkRepository.SaveDrinkChangesAsync();

        return MapDrinkToGetDto(updated);
    }

    public async Task<bool> DeleteDrinkAsync(Guid id)
    {
        var existing = await _drinkRepository.GetDrinkByIdAsyncAsNoTracking(id);
        if (existing is null) return false;

        await _drinkRepository.DeleteDrinkAsync(id);
        await _drinkRepository.SaveDrinkChangesAsync();

        return true;
    }

    public async Task<bool> DrinkExistAsync(Guid id)
    {
        return await _drinkRepository.DrinkExistAsync(id);
    }

    public async Task<IEnumerable<DrinkGetResponseDto>> GetFilterAsync(Guid? brandId, int? minPrice)
    {
        var drinks = await _drinkRepository.GetDrinksAsync();
        var filtered = drinks.AsQueryable();

        if (brandId.HasValue)
        {
            filtered = filtered.Where(d => d.BrandId == brandId);
        }

        if (minPrice.HasValue)
        {
            filtered = filtered.Where(d => d.Price >= minPrice);
        }

        return filtered.Select(MapDrinkToGetDto).ToList();
    }

    private static DrinkGetResponseDto MapDrinkToGetDto(Drink drink)
    {
        return new DrinkGetResponseDto
        {
            Id = drink.Id,
            Name = drink.Name,
            Price = drink.Price,
            Quantity = drink.Quantity,
            ImagePath = drink.ImagePath,
            BrandId = drink.BrandId,
            BrandName = drink.Brand?.Name
        };
    }
}
