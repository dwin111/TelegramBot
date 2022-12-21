using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain.Models;

namespace TelegramBot.DAL
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<TelegramUser> Users { get; set; } 
        public DbSet<TelegramUserMessage> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
