using Auctera.Persistance;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AucteraDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Postgres")
            ));

        return services;
    }
}
