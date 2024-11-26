using System.ComponentModel.DataAnnotations;

namespace HenriksHobbyLager.Models
{
      public class Product
      {
            [Key]
            public int Id { get; set; } // Primärnyckel
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string Category { get; set; }
            public DateTime Created { get; set; }
            public DateTime? LastUpdated { get; set; }
      }
}