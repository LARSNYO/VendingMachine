using Domain.Entities;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.DTOs.Coin;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CoinService : ICoinService
{
    private readonly ICoinRepository _coinRepository;

    public CoinService(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
    }

    public async Task<IEnumerable<CoinGetResponseDto>> GetCoinsAsync()
    {
        var coins = await _coinRepository.GetCoinsAsync();
        return coins.Select(MapCoinToGetDto);
    }

    public async Task<CoinGetResponseDto?> GetByDenominationAsync(int denomination)
    {
        var coin = await _coinRepository.GetByDenominationAsync(denomination);
        return coin is null ? null : MapCoinToGetDto(coin);
    }

    public async Task AddCoinsAsync(Dictionary<int, int> insertedCoins)
    {
        foreach (var (denomination, quantity) in insertedCoins)
        {
            var coin = await _coinRepository.GetByDenominationAsync(denomination);
            if (coin != null)
            {
                coin.Quantity += quantity;
                _coinRepository.UpdateCoin(coin);
            }
        }
        await _coinRepository.SaveCoinChangesAsync();
    }

    public async Task<bool> TryTakeChangeAsync(Dictionary<int, int> changeCoins)
    {
        foreach (var (denomination, quantity) in changeCoins)
        {
            var coin = await _coinRepository.GetByDenominationAsync(denomination);
            if (coin == null || coin.Quantity < quantity)
            {
                return false;
            }
        }

        foreach (var (denomination, quantity) in changeCoins)
        {
            var coin = await _coinRepository.GetByDenominationAsync(denomination);
            if (coin != null)
            {
                coin.Quantity -= quantity;
                _coinRepository.UpdateCoin(coin);
            }
        }

        await _coinRepository.SaveCoinChangesAsync();
        return true;
    }

    public async Task<CoinGetResponseDto?> GetCoinByIdAsyncAsNoTracking(Guid id)
    {
        var coin = await _coinRepository.GetCoinByIdAsyncAsNoTracking(id);
        return coin is null ? null : MapCoinToGetDto(coin);
    }

    public async Task<CoinGetResponseDto> CreateCoinAsync(CoinPostResponseDto dto)
    {
        var coin = new Coin
        {
            Id = Guid.NewGuid(),
            Denomination = dto.Denomination,
            Quantity = dto.Quantity
        };

        await _coinRepository.AddCoinAsync(coin);
        await _coinRepository.SaveCoinChangesAsync();

        return MapCoinToGetDto(coin);
    }

    public async Task<CoinGetResponseDto?> UpdateCoinAsync(Guid id, CoinPostResponseDto dto)
    {
        var existing = await _coinRepository.GetCoinByIdAsyncAsNoTracking(id);
        if (existing is null) return null;

        var updated = existing with
        {
            Denomination = dto.Denomination,
            Quantity = dto.Quantity
        };

        _coinRepository.UpdateCoin(updated);
        await _coinRepository.SaveCoinChangesAsync();

        return MapCoinToGetDto(updated);
    }

    public async Task<bool> DeleteCoinAsync(Guid id)
    {
        var existing = await _coinRepository.GetCoinByIdAsyncAsNoTracking(id);
        if (existing is null) return false;

        await _coinRepository.DeleteCoinAsync(id);
        await _coinRepository.SaveCoinChangesAsync();

        return true;
    }

    public async Task<bool> CoinExistAsync(Guid id)
    {
        return await _coinRepository.CoinExistAsync(id);
    }

    private static CoinGetResponseDto MapCoinToGetDto(Coin coin)
    {
        return new CoinGetResponseDto
        {
            Id = coin.Id,
            Denomination = coin.Denomination,
            Quantity = coin.Quantity
        };
    }
}
