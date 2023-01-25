using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace TodoApp.Api.Db
{
    public class TodoAppDbContextFactory : IDesignTimeDbContextFactory<TodoAppDbContext>
    {

        public TodoAppDbContext CreateDbContext(string[] args)
        {
            //Commands for database
            //dotnet ef migrations add Initial -- "Server=(localdb)\MSSQLLocalDB;Database=TodoApp;"
            //dotnet ef database update -- "Server=(localdb)\MSSQLLocalDB;Database=TodoApp;"

            var optionsBuilder = new DbContextOptionsBuilder<TodoAppDbContext>();
            optionsBuilder.UseSqlServer(args[0]);
            return new TodoAppDbContext(optionsBuilder.Options);
        }
    }
}
