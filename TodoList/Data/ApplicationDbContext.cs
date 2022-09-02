using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    // to connect to Db
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
