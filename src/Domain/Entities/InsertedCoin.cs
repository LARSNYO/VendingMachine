namespace Domain.Entities;

public record class InsertedCoin
{
    public int Denomination { get; init; }
    public int Quantity { get; init; }
}
