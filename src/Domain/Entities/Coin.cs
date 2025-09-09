namespace Domain.Entities;

public record class Coin
{
    public Guid Id { get; init; }
    public int Denomination { get; set; }
    public int Quantity { get; set; }
}
