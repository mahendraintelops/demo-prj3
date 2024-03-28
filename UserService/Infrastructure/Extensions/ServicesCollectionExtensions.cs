using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;


namespace Infrastructure.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("ConnectionString")));
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
            return serviceCollection;
        }
    }
}
