namespace ShoppingCart.Domain;

public class Order
{
    public int Id { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerEmail { get; set; }
    public required string ShippingAddress { get; set; }
    public required string City { get; set; }
    public string? Notes { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
