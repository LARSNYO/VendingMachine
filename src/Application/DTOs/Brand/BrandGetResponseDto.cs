using Application.DTOs.Drink;

namespace Application.DTOs.Brand;

public record class BrandGetResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public ICollection<DrinkGetResponseDto>? Drinks { get; init; }
}