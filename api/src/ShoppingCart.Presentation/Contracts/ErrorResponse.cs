namespace ShoppingCart.Presentation.Contracts;

public sealed record ErrorResponse(string Message, string? Detail = null);