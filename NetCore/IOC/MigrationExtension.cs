using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechTestData.Data;

namespace IOC
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app) 
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DBContext dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();

            dbContext.Database.Migrate();
        }
    }
}
