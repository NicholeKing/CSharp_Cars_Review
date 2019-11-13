using Microsoft.EntityFrameworkCore;
 
namespace Cars.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Car> Autos {get;set;}
    }
}