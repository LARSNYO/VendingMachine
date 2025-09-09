namespace Application.DTOs.Drink;

public record class DrinkPostRequestDto
{
    public string Name { get; init; } = "";
    public int Price { get; init; }
    public int Quantity { get; init; }
    public string? ImagePath { get; init; }
    public Guid BrandId { get; init; }
}
