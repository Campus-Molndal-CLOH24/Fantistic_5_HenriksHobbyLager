using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Database
{
    public class SqliteDbcontext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HenriksHobbyLager.db");
        }

        public void EnsureProductsTableExists()
        {
            using (var connection = Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                // Check if the "Products" table exists
                command.CommandText = @"
                    SELECT COUNT(*) 
                    FROM sqlite_master 
                    WHERE type = 'table' AND name = 'Products'";

                var tableExists = Convert.ToInt32(command.ExecuteScalar()) > 0;

                if (!tableExists)
                {
                    // Create the "Products" table
                    command.CommandText = @"
                        CREATE TABLE Products (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Price REAL NOT NULL,
                            Stock INTEGER NOT NULL,
                            Category TEXT NOT NULL,
                            Created DATETIME NOT NULL,
                            LastUpdated DATETIME
                        )";
                    command.ExecuteNonQuery();

                    Console.WriteLine("Products table created.");
                }
                else
                {
                    Console.WriteLine("Products table already exists.");
                }
            }
        }

    }
}

