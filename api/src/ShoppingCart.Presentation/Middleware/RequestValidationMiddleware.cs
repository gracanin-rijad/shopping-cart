using System.Text.Json;
using ShoppingCart.Presentation.Contracts;

namespace ShoppingCart.Presentation.Middleware;

public class RequestValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (IsWriteMethod(context.Request.Method) &&
            context.Request.ContentLength.GetValueOrDefault() > 0 &&
            !context.Request.HasJsonContentType())
        {
            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse("Request content type must be application/json.");
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }

        await next(context);
    }

    private static bool IsWriteMethod(string method)
    {
        return HttpMethods.IsPost(method) ||
               HttpMethods.IsPut(method) ||
               HttpMethods.IsPatch(method);
    }
}