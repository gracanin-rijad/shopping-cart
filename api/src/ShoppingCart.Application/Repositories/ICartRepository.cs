using ShoppingCart.Domain;

namespace ShoppingCart.Application.Repositories;

public interface ICartRepository
{
    Task<IEnumerable<CartItem>> GetItemsAsync();
    Task<CartItem?> GetItemAsync(int itemId);
    Task<CartItem?> GetItemByProductIdAsync(int productId);
    Task<CartItem> AddAsync(CartItem item);
    Task<CartItem> UpdateAsync(CartItem item);
    Task<bool> DeleteAsync(int itemId);
}
