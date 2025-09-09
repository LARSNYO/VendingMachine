namespace Domain.Entities;

public record class Brand
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public ICollection<Drink>? Drinks { get; init; }
}
