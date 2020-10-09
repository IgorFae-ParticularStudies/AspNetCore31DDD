using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class DbTeste : IDisposable
    {

        // Para criar um banco diferente a cada teste
        private string dataBaseName = $"dbApi_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider ServiceProvider { get; private set; }

        public DbTeste()
        {
            // Configuramos o service
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
                o.UseSqlServer($"Server=.\\SQLEXPRESS;Initial Catalog={dataBaseName};MultipleActiveResultSets=true;User ID=sa;Password=123456"),
                ServiceLifetime.Transient
            );

            // Build do service
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Criação de um novo banco
            using (var context = ServiceProvider.GetService<MyContext>())
            {

                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            // Destruindo o banco
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }

    }
}
