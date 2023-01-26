using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Db.Entities;

namespace TodoApp.Api.Db
{
    public class TodoAppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<SendEmailRequestEntity> SendEmailRequests { get; set; }


        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options)
        {


        }

    }
}
