using ShoppingCart.Application.Repositories;
using ShoppingCart.Application.Requests;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository cartRepository;
    private readonly IProductService productService;

    public CartService(ICartRepository cartRepository, IProductService productService)
    {
        this.cartRepository = cartRepository;
        this.productService = productService;
    }

    public async Task<CartDto> GetCartAsync()
    {
        var items = await cartRepository.GetItemsAsync();
        return await BuildCartDtoAsync(items);
    }

    public async Task<CartDto?> AddItemAsync(AddCartItemRequest request)
    {
        var product = await productService.GetProductByIdAsync(request.ProductId);
        if (product is null)
            return null;

        var existingItem = await cartRepository.GetItemByProductIdAsync(request.ProductId);
        if (existingItem is not null)
        {
            existingItem.Quantity += request.Quantity;
            await cartRepository.UpdateAsync(existingItem);
        }
        else
        {
            var item = new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await cartRepository.AddAsync(item);
        }

        var items = await cartRepository.GetItemsAsync();
        return await BuildCartDtoAsync(items);
    }

    public async Task<CartDto?> UpdateItemAsync(int itemId, UpdateCartItemRequest request)
    {
        var item = await cartRepository.GetItemAsync(itemId);
        if (item is null)
            return null;

        item.Quantity = request.Quantity;
        await cartRepository.UpdateAsync(item);

        var items = await cartRepository.GetItemsAsync();
        return await BuildCartDtoAsync(items);
    }

    public async Task<CartDto?> RemoveItemAsync(int itemId)
    {
        var deleted = await cartRepository.DeleteAsync(itemId);
        if (!deleted)
            return null;

        var items = await cartRepository.GetItemsAsync();
        return await BuildCartDtoAsync(items);
    }

    private async Task<CartDto> BuildCartDtoAsync(IEnumerable<CartItem> items)
    {
        var cartItems = new List<CartItemDto>();
        var totalPrice = 0m;
        var totalItems = 0;

        foreach (var item in items)
        {
            var product = await productService.GetProductByIdAsync(item.ProductId);
            if (product is null)
                continue;

            var itemTotal = product.Price * item.Quantity;
            totalPrice += itemTotal;
            totalItems += item.Quantity;

            cartItems.Add(new CartItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = item.Quantity,
                TotalPrice = itemTotal,
                ImageUrl = product.ImageUrl
            });
        }

        return new CartDto
        {
            Items = cartItems,
            TotalItems = totalItems,
            TotalPrice = totalPrice
        };
    }
}
