namespace ShoppingCart.Application;

public class CartDto
{
    public IEnumerable<CartItemDto> Items { get; set; } = Enumerable.Empty<CartItemDto>();
    public int TotalItems { get; set; }
    public decimal TotalPrice { get; set; }
}
