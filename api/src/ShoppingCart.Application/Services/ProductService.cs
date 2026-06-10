namespace ShoppingCart.Application.Services;

public class ProductService : IProductService
{
    private static readonly List<ProductDto> Products = new()
    {
        new ProductDto
        {
            Id = 1,
            Name = "Wireless Headphones",
            Description = "High-quality wireless headphones with noise cancellation",
            Price = 79.99m,
            StockQuantity = 50,
            ImageUrl = "https://images.unsplash.com/photo-1512314889357-e157c22f938d?auto=format&fit=crop&w=400&q=80"
        },
        new ProductDto
        {
            Id = 2,
            Name = "USB-C Cable",
            Description = "Durable 6ft USB-C charging and data cable",
            Price = 14.99m,
            StockQuantity = 200,
            ImageUrl = "https://images.unsplash.com/photo-1512314889357-e157c22f938d?auto=format&fit=crop&w=400&q=80"
        },
        new ProductDto
        {
            Id = 3,
            Name = "Phone Case",
            Description = "Protective phone case with shock absorption",
            Price = 24.99m,
            StockQuantity = 150,
            ImageUrl = "https://images.unsplash.com/photo-1512314889357-e157c22f938d?auto=format&fit=crop&w=400&q=80"
        },
        new ProductDto
        {
            Id = 4,
            Name = "Screen Protector",
            Description = "Tempered glass screen protector with easy installation",
            Price = 9.99m,
            StockQuantity = 300,
            ImageUrl = "https://images.unsplash.com/photo-1512314889357-e157c22f938d?auto=format&fit=crop&w=400&q=80"
        },
        new ProductDto
        {
            Id = 5,
            Name = "Portable Charger",
            Description = "20000mAh portable power bank with fast charging",
            Price = 49.99m,
            StockQuantity = 75,
            ImageUrl = "https://images.unsplash.com/photo-1512314889357-e157c22f938d?auto=format&fit=crop&w=400&q=80"
        }
    };

    public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return Task.FromResult(Products.AsEnumerable());
    }

    public Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }
}
