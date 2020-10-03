using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para criar as migrações
            //var connectionString = "Server=localhost;Port=3306;Database=DbApi;Uid=root;Pwd=igordev78";
            var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=DbApi;MultipleActiveResultSets=true;User ID=sa;Password=123456";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            //optionsBuilder.UseMySql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
