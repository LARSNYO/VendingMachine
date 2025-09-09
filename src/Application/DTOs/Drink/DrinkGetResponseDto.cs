namespace Application.DTOs.Drink;

public record class DrinkGetResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public int Price { get; init; }
    public int Quantity { get; init; }
    public string? ImagePath { get; init; }
    public Guid BrandId { get; init; }
    public string? BrandName{ get; init; }
}
