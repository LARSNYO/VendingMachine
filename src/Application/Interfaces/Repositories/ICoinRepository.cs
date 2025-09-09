using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICoinRepository
{
    Task<IEnumerable<Coin>> GetCoinsAsync();
    Task<Coin?> GetCoinByIdAsync(Guid id);
    Task<Coin?> GetByDenominationAsync(int denomination);
    Task AddCoinAsync(Coin coin);
    void UpdateCoin(Coin coin);
    Task DeleteCoinAsync(Guid id);
    Task SaveCoinChangesAsync();
    Task<bool> CoinExistAsync(Guid id);
    Task<IEnumerable<Coin>> GetAvailableCoinsAsync();
}
