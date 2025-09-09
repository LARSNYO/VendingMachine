using Domain.Entities;

namespace Application.Interfaces.Services;

public interface ICartService
{
    string GetCartId();
    Task<IEnumerable<CartItem>> GetCartItemsAsync();
    Task<int> GetCartTotalAsync();
    Task AddToCartAsync(Drink drink, int quantity);
    Task<int> IncreaseQuantityAsync(Drink drink);
    Task<int> ReduceQuantityAsync(Drink drink);
    Task ClearCart();

}
