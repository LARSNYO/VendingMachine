using Domain.Entities;

namespace Application.Interfaces.Services;

public interface ICoinService
{
    Task<IEnumerable<Coin>> GetCoinsAsync();
    Task<Coin?> GetByDenominationAsync(int denomination);
    Task AddCoinsAsync(Dictionary<int, int> insertCoins);
    Task<Coin?> GetCoinByIdAsync(Guid id);
    Task CreateCoinAsync(Coin coin);
    Task UpdateCoinAsync(Coin coin);
    Task DeleteCoinAsync(Guid id);
    Task<bool> CoinExistAsync(Guid id);
    Task<bool> TryTakeChangeAsync(Dictionary<int, int> changeCoins);
}
