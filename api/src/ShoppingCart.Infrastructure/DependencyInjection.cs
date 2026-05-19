using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Infrastructure.Persistence;

namespace ShoppingCart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ShoppingCartDbContext>(options =>
            options.UseInMemoryDatabase("ShoppingCartDb"));

        return services;
    }
}