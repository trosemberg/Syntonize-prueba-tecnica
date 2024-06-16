using Microsoft.Extensions.DependencyInjection;
using TechTestData.Models;
using TechTestData.Repositories.Interface;
using TechTestData.Repositories;

namespace IOC
{
    public static class RepositoresExtensions
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IRepository<Users>, Repository<Users>>();
            service.AddScoped<IRepository<Roles>, Repository<Roles>>();
            service.AddScoped<IRepository<Products>, Repository<Products>>();
        }
    }
}
