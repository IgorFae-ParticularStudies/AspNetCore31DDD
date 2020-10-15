using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {

        public MyContext CreateDbContext(string[] args)
        {
            // Configuração para ler o launch.json
            var builder = new ConfigurationBuilder()
             .SetBasePath(@"C:/Users/igors/Box/01-CursosOnline/55-Udemy/01-AspNetCore31ApiComDDDnaPratica/curso_api_netcore/.vscode")
             .AddJsonFile("launch.json")
             .Build();

            // Aqui pego o valor que está dentro da Key DATABASE
            var databaseName = builder["configurations:0:env:DATABASE"];

            if (databaseName.ToLower() == "SQLSERVER".ToLower())
            {
                var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=DbApi;MultipleActiveResultSets=true;User ID=sa;Password=123456";
                var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new MyContext(optionsBuilder.Options);
            }
            else
            {
                //Usado para criar as migrações
                var connectionString = "Server=localhost;Port=3306;Database=DbApi;Uid=root;Pwd=igordev78";
                var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
                optionsBuilder.UseMySql(connectionString);
                return new MyContext(optionsBuilder.Options);
            }

        }
    }
}
