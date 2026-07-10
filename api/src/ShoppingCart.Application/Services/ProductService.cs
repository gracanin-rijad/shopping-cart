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
            ImageUrl = "https://myhypergear.com/cdn/shop/products/15613_HYG_Vibe_Wireless_Headphones_White_001.jpg?v=1644943914"
        },
        new ProductDto
        {
            Id = 2,
            Name = "USB-C Cable",
            Description = "Durable 6ft USB-C charging and data cable",
            Price = 14.99m,
            StockQuantity = 200,
            ImageUrl = "https://media.startech.com/cms/products/main/ucc-3m-10g-usb-cable.main.jpg"
        },
        new ProductDto
        {
            Id = 3,
            Name = "Phone Case",
            Description = "Protective phone case with shock absorption",
            Price = 24.99m,
            StockQuantity = 150,
            ImageUrl = "https://harperandblake.co.uk/cdn/shop/files/i-Phone-15-Tough-Mag-Safe-Case---Front-View---Website-Cute-Sun---Yellow.jpg?v=1705578880"
        },
        new ProductDto
        {
            Id = 4,
            Name = "Screen Protector",
            Description = "Tempered glass screen protector with easy installation",
            Price = 9.99m,
            StockQuantity = 300,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ64vmUH9AGJl1NjNPy8xDD6F4npE2FQ1wTkLRpHqsmCg&s=10"
        },
        new ProductDto
        {
            Id = 5,
            Name = "Portable Charger",
            Description = "20000mAh portable power bank with fast charging",
            Price = 49.99m,
            StockQuantity = 75,
            ImageUrl = "https://zyrontech.com.au/cdn/shop/files/powaflex-20000mah-power-bank-941913.jpg?v=1726767719&width=2048"
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
