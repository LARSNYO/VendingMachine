namespace Domain.Entities;

public record class Drink
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public int Price { get; init; }
    public int Quantity { get; init; }
    public string? ImagePath { get; init; }
    public Guid BrandId { get; init; }
    public Brand? Brand { get; init; }
}
