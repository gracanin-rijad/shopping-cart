using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ShoppingCart.Application.Requests;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Domain;
using Xunit;

namespace ShoppingCart.Application.Tests;

public class CartServiceTests
{
    [Fact]
    public async Task AddItemAsync_ReturnsNull_WhenProductNotFound()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        productService.Setup(p => p.GetProductByIdAsync(1)).ReturnsAsync((ProductDto?)null);

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.AddItemAsync(new AddCartItemRequest { ProductId = 1, Quantity = 2 });

        Assert.Null(result);
        cartRepo.Verify(r => r.AddAsync(It.IsAny<CartItem>()), Times.Never);
    }

    [Fact]
    public async Task AddItemAsync_AddsNewItem_WhenProductExistsAndNoExistingItem()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        productService.Setup(p => p.GetProductByIdAsync(1)).ReturnsAsync(new ProductDto { Id = 1, Name = "P", Price = 10m, ImageUrl = null });
        cartRepo.Setup(r => r.GetItemByProductIdAsync(1)).ReturnsAsync((CartItem?)null);
        cartRepo.Setup(r => r.AddAsync(It.IsAny<CartItem>())).ReturnsAsync((CartItem item) => { item.Id = 1; return item; });
        cartRepo.Setup(r => r.GetItemsAsync()).ReturnsAsync(new List<CartItem> { new CartItem { Id = 1, ProductId = 1, Quantity = 2 } });

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.AddItemAsync(new AddCartItemRequest { ProductId = 1, Quantity = 2 });

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalItems);
        Assert.Equal(20m, result.TotalPrice);
        cartRepo.Verify(r => r.AddAsync(It.Is<CartItem>(i => i.ProductId == 1 && i.Quantity == 2)), Times.Once);
    }

    [Fact]
    public async Task GetCartAsync_ReturnsCart_WhenItemsExist()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        cartRepo.Setup(r => r.GetItemsAsync()).ReturnsAsync(new List<CartItem> { new CartItem { Id = 1, ProductId = 1, Quantity = 3 } });
        productService.Setup(p => p.GetProductByIdAsync(1)).ReturnsAsync(new ProductDto { Id = 1, Name = "P", Price = 5m });

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.GetCartAsync();

        Assert.NotNull(result);
        Assert.Equal(3, result.TotalItems);
        Assert.Equal(15m, result.TotalPrice);
        Assert.True(logger.Invocations.Any(inv => inv.Arguments.Count > 2 && inv.Arguments[2]?.ToString().Contains("GetCartAsync called") == true));
    }

    [Fact]
    public async Task UpdateItemAsync_ReturnsNull_WhenItemNotFound()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        cartRepo.Setup(r => r.GetItemAsync(1)).ReturnsAsync((CartItem?)null);

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.UpdateItemAsync(1, new UpdateCartItemRequest { Quantity = 5 });

        Assert.Null(result);
        Assert.True(logger.Invocations.Any(inv => inv.Arguments.Count > 2 && inv.Arguments[2]?.ToString().Contains("Cart item not found") == true));
    }

    [Fact]
    public async Task UpdateItemAsync_UpdatesItem_WhenFound()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        cartRepo.Setup(r => r.GetItemAsync(1)).ReturnsAsync(new CartItem { Id = 1, ProductId = 1, Quantity = 2 });
        cartRepo.Setup(r => r.UpdateAsync(It.IsAny<CartItem>())).ReturnsAsync((CartItem i) => i);
        cartRepo.Setup(r => r.GetItemsAsync()).ReturnsAsync(new List<CartItem> { new CartItem { Id = 1, ProductId = 1, Quantity = 5 } });
        productService.Setup(p => p.GetProductByIdAsync(1)).ReturnsAsync(new ProductDto { Id = 1, Name = "P", Price = 2m });

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.UpdateItemAsync(1, new UpdateCartItemRequest { Quantity = 5 });

        Assert.NotNull(result);
        Assert.Equal(5, result.TotalItems);
        Assert.Equal(10m, result.TotalPrice);
        Assert.True(logger.Invocations.Any(inv => inv.Arguments.Count > 2 && inv.Arguments[2]?.ToString().Contains("UpdateItemAsync called") == true));
    }

    [Fact]
    public async Task RemoveItemAsync_ReturnsNull_WhenDeleteFails()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        cartRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(false);

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.RemoveItemAsync(1);

        Assert.Null(result);
        Assert.True(logger.Invocations.Any(inv => inv.Arguments.Count > 2 && inv.Arguments[2]?.ToString().Contains("Failed to delete cart item") == true));
    }

    [Fact]
    public async Task RemoveItemAsync_DeletesAndReturnsCart_WhenSuccess()
    {
        var cartRepo = new Mock<ICartRepository>();
        var productService = new Mock<IProductService>();
        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ShoppingCart.Application.Services.CartService>>();

        cartRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
        cartRepo.Setup(r => r.GetItemsAsync()).ReturnsAsync(new List<CartItem>());

        var service = new ShoppingCart.Application.Services.CartService(cartRepo.Object, productService.Object, logger.Object);

        var result = await service.RemoveItemAsync(1);

        Assert.NotNull(result);
        Assert.Equal(0, result.TotalItems);
        Assert.Equal(0m, result.TotalPrice);
        Assert.True(logger.Invocations.Any(inv => inv.Arguments.Count > 2 && inv.Arguments[2]?.ToString().Contains("RemoveItemAsync called") == true));
    }
}
