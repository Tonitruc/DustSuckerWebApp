using DustSuckerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DustSuckerWebApp.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        public DbSet<Hoover> Hoovers { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
