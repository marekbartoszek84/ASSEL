using Assel.Contacts.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.WebApi.Extensions
{
    public static class SqlContextExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ContactDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("Assel.Contacts.Infrastructure")));
    }
}
