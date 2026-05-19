using System.Text.Json;
using ShoppingCart.Presentation.Contracts;

namespace ShoppingCart.Presentation.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse(
                "An unexpected error occurred.",
                context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment()
                    ? exception.Message
                    : null);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}