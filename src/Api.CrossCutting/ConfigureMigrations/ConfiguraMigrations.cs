using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.ConfigureMigrations
{
    public class ConfiguraMigrations
    {
        public static void Configure(IServiceScope service)
        {
            using (var context = service.ServiceProvider.GetService<MyContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}
