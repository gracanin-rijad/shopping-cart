using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Infrastructure.Persistence;
using ShoppingCart.Infrastructure.Repositories;

namespace ShoppingCart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ShoppingCartDbContext>(options =>
            options.UseInMemoryDatabase("ShoppingCartDb"));

        services.AddScoped<ICartRepository, InMemoryCartRepository>();

        return services;
    }
}