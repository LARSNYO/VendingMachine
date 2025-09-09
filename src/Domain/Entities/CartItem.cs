namespace Domain.Entities;

public record class CartItem
{
    public Guid Id { get; init; }
    public int Quantity { get; set; }
    public Drink? Drink { get; init; }
    public string CartId { get; init; } = null!;
}
