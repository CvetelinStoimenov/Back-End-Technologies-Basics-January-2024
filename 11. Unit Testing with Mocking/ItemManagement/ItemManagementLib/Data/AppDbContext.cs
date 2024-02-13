using ItemManagementLib.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemManagementLib.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-23QTAUK\\SQLEXPRESS;Database=ItemManagement;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Item> Items { get; set; }
    }
}
