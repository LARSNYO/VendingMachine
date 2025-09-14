using Application.DTOs.Coin;

namespace Application.Interfaces.Services;

public interface ICoinService
{
    Task<IEnumerable<CoinGetResponseDto>> GetCoinsAsync();
    Task<CoinGetResponseDto?> GetByDenominationAsync(int denomination);
    Task AddCoinsAsync(Dictionary<int, int> insertCoins);
    Task<CoinGetResponseDto?> GetCoinByIdAsyncAsNoTracking(Guid id);
    Task<CoinGetResponseDto> CreateCoinAsync(CoinPostResponseDto dto);
    Task<CoinGetResponseDto?> UpdateCoinAsync(Guid id, CoinPostResponseDto dto);
    Task<bool> DeleteCoinAsync(Guid id);
    Task<bool> CoinExistAsync(Guid id);
    Task<bool> TryTakeChangeAsync(Dictionary<int, int> changeCoins);
}
