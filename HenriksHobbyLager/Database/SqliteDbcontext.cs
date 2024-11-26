using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Database
{
    public class SqliteDbcontext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HenriksBalleLager.db");
        }

    }
}

