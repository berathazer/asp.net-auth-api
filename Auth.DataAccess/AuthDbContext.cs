using Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.DataAccess
{
    public class AuthDbContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=BERAA;Initial Catalog=dotnet;User Id=sa;Password=Berat102030;Encrypt=true;TrustServerCertificate=true;");
        }

        public DbSet<User> Users { get; set; }
       
        public DbSet<Post> Posts { get; set; }


    }
}
