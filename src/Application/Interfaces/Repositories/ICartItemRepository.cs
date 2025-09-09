using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<CartItem?> GetCartItemByIdAsync(string cartId, Guid drinkId);
    Task<IEnumerable<CartItem>> GetCartItemsAsync(string cartId);
    Task<int> GetCartTotalAsync(string cartId);
    Task AddCartItemAsync(CartItem cartItem);
    void UpdateCartItem(CartItem cartItem);
    void RemoveCartItem(CartItem cartItem);
    void ClearCart(string cartId);
    Task SaveCartItemChangesAsync();

}
