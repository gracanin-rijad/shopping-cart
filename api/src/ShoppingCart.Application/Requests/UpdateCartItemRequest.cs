using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Application.Requests;

public class UpdateCartItemRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
