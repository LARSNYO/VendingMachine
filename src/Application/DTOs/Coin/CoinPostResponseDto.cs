namespace Application.DTOs.Coin;

public record class CoinPostResponseDto
{
    public int Denomination { get; init; }
    public int Quantity { get; init; }
}

