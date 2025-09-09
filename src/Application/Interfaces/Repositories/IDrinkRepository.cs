using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IDrinkRepository
{
    Task<IEnumerable<Drink>> GetDrinksAsync();
    Task<Drink?> GetDrinkByIdAsyncAsNoTracking(Guid id);
    Task<Drink?> GetDrinkWithBrandByIdAsync(Guid id);
    Task AddDrinkAsync(Drink drink);
    void UpdateDrink(Drink drink);
    Task DeleteDrinkAsync(Guid id);
    Task SaveDrinkChangesAsync();
    Task<bool> DrinkExistAsync(Guid id);
}
