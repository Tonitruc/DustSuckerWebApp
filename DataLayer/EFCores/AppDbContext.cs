using DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EFCores
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        public DbSet<Hoover> Hoovers { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<Favorite> Favorites { get; set; }
    }
}
