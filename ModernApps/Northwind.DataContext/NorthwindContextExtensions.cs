using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.EntityModels;

public static class NorthwindContextExtensions
{
    /// <summary>
    /// Adds NorthwindContext to the specified IServiceCollection. Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">Set to override the default</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddNorthwindContext(this IServiceCollection services, string? connectionString = null)
    {
        if(connectionString is null)
        {
             SqlConnectionStringBuilder builder = new()
            {
                DataSource = "tcp:127.0.0.1,1433", //container
                InitialCatalog = "Northwind",
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                //To fail faster. Default is 15.
                ConnectTimeout = 3,
                UserID = Environment.GetEnvironmentVariable("MY_SQL_USR"),
                Password = Environment.GetEnvironmentVariable("MY_SQL_PWD")
            };
            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<NorthwindContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.LogTo(NorthwindContextLogger.WriteLine, new[]
            {
                Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting
            });
        },
        //to avoid concurrency issues with Blazor Server Projects.
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Transient);
        
        return services;
    }
}
