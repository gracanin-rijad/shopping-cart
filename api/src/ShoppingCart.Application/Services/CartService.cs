using ShoppingCart.Application.Repositories;
using ShoppingCart.Application.Requests;
using ShoppingCart.Domain;
using Microsoft.Extensions.Logging;

namespace ShoppingCart.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository cartRepository;
    private readonly IProductService productService;
    private readonly ILogger<CartService> logger;

    public CartService(ICartRepository cartRepository, IProductService productService, ILogger<CartService> logger)
    {
        this.cartRepository = cartRepository;
        this.productService = productService;
        this.logger = logger;
    }

    public async Task<CartDto> GetCartAsync()
    {
        logger.LogInformation("GetCartAsync called");
        var items = await cartRepository.GetItemsAsync();
        var dto = await BuildCartDtoAsync(items);
        logger.LogInformation("GetCartAsync completed. TotalItems={TotalItems} TotalPrice={TotalPrice}", dto.TotalItems, dto.TotalPrice);
        return dto;
    }

    public async Task<CartDto?> AddItemAsync(AddCartItemRequest request)
    {
        logger.LogInformation("AddItemAsync called for ProductId={ProductId} Quantity={Quantity}", request.ProductId, request.Quantity);
        var product = await productService.GetProductByIdAsync(request.ProductId);
        if (product is null)
        {
            logger.LogWarning("Product not found: {ProductId}", request.ProductId);
            return null;
        }

        var existingItem = await cartRepository.GetItemByProductIdAsync(request.ProductId);
        if (existingItem is not null)
        {
            logger.LogInformation("Updating existing cart item (Id={ItemId}) quantity {OldQty} -> {NewQty}", existingItem.Id, existingItem.Quantity, existingItem.Quantity + request.Quantity);
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

            logger.LogInformation("Adding new cart item for ProductId={ProductId} Quantity={Quantity}", request.ProductId, request.Quantity);
            await cartRepository.AddAsync(item);
        }

        var items = await cartRepository.GetItemsAsync();
        return await BuildCartDtoAsync(items);
    }

    public async Task<CartDto?> UpdateItemAsync(int itemId, UpdateCartItemRequest request)
    {
        logger.LogInformation("UpdateItemAsync called for ItemId={ItemId} Quantity={Quantity}", itemId, request.Quantity);
        var item = await cartRepository.GetItemAsync(itemId);
        if (item is null)
        {
            logger.LogWarning("Cart item not found: {ItemId}", itemId);
            return null;
        }

        item.Quantity = request.Quantity;
        await cartRepository.UpdateAsync(item);

        logger.LogInformation("Cart item updated: ItemId={ItemId} NewQuantity={Quantity}", itemId, item.Quantity);

        var items = await cartRepository.GetItemsAsync();
        var dto = await BuildCartDtoAsync(items);
        logger.LogInformation("UpdateItemAsync completed. TotalItems={TotalItems} TotalPrice={TotalPrice}", dto.TotalItems, dto.TotalPrice);
        return dto;
    }

    public async Task<CartDto?> RemoveItemAsync(int itemId)
    {
        logger.LogInformation("RemoveItemAsync called for ItemId={ItemId}", itemId);
        var deleted = await cartRepository.DeleteAsync(itemId);
        if (!deleted)
        {
            logger.LogWarning("Failed to delete cart item: {ItemId}", itemId);
            return null;
        }

        logger.LogInformation("Cart item deleted: ItemId={ItemId}", itemId);
        var items = await cartRepository.GetItemsAsync();
        var dto = await BuildCartDtoAsync(items);
        logger.LogInformation("RemoveItemAsync completed. TotalItems={TotalItems} TotalPrice={TotalPrice}", dto.TotalItems, dto.TotalPrice);
        return dto;
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
            {
                logger.LogWarning("Product for cart item not found during BuildCartDtoAsync: {ProductId}", item.ProductId);
                continue;
            }

            var itemTotal = product.Price * item.Quantity;
            totalPrice += itemTotal;
            totalItems += item.Quantity;

            logger.LogDebug("Adding CartItemDto for ProductId={ProductId} Quantity={Quantity} ItemTotal={ItemTotal}", product.Id, item.Quantity, itemTotal);
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

        logger.LogInformation("BuildCartDtoAsync completed. TotalItems={TotalItems} TotalPrice={TotalPrice}", totalItems, totalPrice);

        return new CartDto
        {
            Items = cartItems,
            TotalItems = totalItems,
            TotalPrice = totalPrice
        };
    }
}
