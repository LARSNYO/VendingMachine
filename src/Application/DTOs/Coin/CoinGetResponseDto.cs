namespace Application.DTOs.Coin;

public record class CoinGetResponseDto
{
    public Guid Id { get; init; }
    public int Denomination { get; init; }
    public int Quantity { get; init; }
}