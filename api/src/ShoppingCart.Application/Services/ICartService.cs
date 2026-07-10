using ShoppingCart.Application.Requests;

namespace ShoppingCart.Application.Services;

public interface ICartService
{
    Task<CartDto> GetCartAsync();
    Task<CartDto?> AddItemAsync(AddCartItemRequest request);
    Task<CartDto?> UpdateItemAsync(int itemId, UpdateCartItemRequest request);
    Task<CartDto?> RemoveItemAsync(int itemId);
}
