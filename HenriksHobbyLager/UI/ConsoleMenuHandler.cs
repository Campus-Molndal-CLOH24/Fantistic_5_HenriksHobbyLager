using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.UI
{
    internal class ConsoleMenuHandler
    {
        public static async Task RunMenuAsync(IProductFacade productFacade)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Henriks HobbyLager™ 1.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök produkter");
                Console.WriteLine("6. Avsluta");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ShowAllProductsAsync(productFacade);
                        break;
                    case "2":
                        await AddProductAsync(productFacade);
                        break;
                    case "3":
                        await UpdateProductAsync(productFacade);
                        break;
                    case "4":
                        await DeleteProductAsync(productFacade);
                        break;
                    case "5":
                        await SearchProductsAsync(productFacade);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private static async Task ShowAllProductsAsync(IProductFacade productFacade)
        {
            var products = await productFacade.GetAllProductsAsync();
            if (!products.Any())
            {
                Console.WriteLine("Inga produkter finns i lagret. Dags att shoppa grossist!");
                return;
            }

            foreach (var product in products)
            {
                DisplayProduct(product);
            }
        }

        private static async Task AddProductAsync(IProductFacade productFacade)
        {
            Console.WriteLine("=== Lägg till ny produkt ===");

            Console.Write("Namn: ");
            var name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name)) 
            {
                Console.WriteLine("Namn får inte vara tomt!");
                Console.Write("Namn: ");
                name = Console.ReadLine();
            }

            Console.Write("Pris: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Ogiltigt pris! Använd komma istället för punkt.");
                Console.Write("Pris: ");
            }

            Console.Write("Antal i lager: ");
            int stock;
            while (!int.TryParse(Console.ReadLine(), out stock))
            {
                Console.WriteLine("Ogiltig lagermängd! Hela tal endast.");
                Console.Write("Antal i lager: ");
            }

            Console.Write("Kategori: ");
            var category = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Kategori får inte vara tomt!");
                Console.Write("Kategori: ");
                category = Console.ReadLine();
            }

            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category,
                Created = DateTime.Now
            };

            await productFacade.CreateProductAsync(product);
            Console.WriteLine("Produkt tillagd!");
        }

        private static async Task UpdateProductAsync(IProductFacade productFacade)
        {
            Console.Write("Ange produkt-ID att uppdatera: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror tack!");
                Console.Write("Ange produkt-ID att uppdatera: ");
            }

            var product = await productFacade.GetProductAsync(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte!");
                return;
            }

            Console.Write("Nytt namn (enter för att behålla): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                product.Name = name;

            decimal price;
            while (true) 
            {
                Console.Write("Nytt pris (enter för att behålla): ");
                var priceInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(priceInput))
                    break;

                if (decimal.TryParse(priceInput, out price) && price > 0)
                {                                       
                    product.Price = price;
                    break;
                }
                Console.WriteLine("Ogiltigt pris! Använd punkt istället för komma.");
            }                            

            Console.Write("Ny lagermängd (enter för att behålla): ");
            var stockInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stockInput)) 
            {
                int stock;
                do
                {
                    Console.WriteLine("Ogiltig lagermängd! Hela tal endast.");
                    Console.Write("Ny lagermängd(enter för att behålla): ");
                }
                while (!int.TryParse(Console.ReadLine(), out stock));
                product.Stock = stock;
            }

            Console.Write("Ny kategori (enter för att behålla): ");
            var category = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(category))
                product.Category = category;

            product.LastUpdated = DateTime.Now;

            await productFacade.UpdateProductAsync(product);
            Console.WriteLine("Produkt uppdaterad!");
        }

        private static async Task DeleteProductAsync(IProductFacade productFacade)
        {
            Console.Write("Ange produkt-ID att ta bort: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror är tillåtna här.");
                return;
            }

            var product = await productFacade.GetProductAsync(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte!");
                return;
            }

            await productFacade.DeleteProductAsync(product.Id);
            Console.WriteLine("Produkt borttagen!");
        }

        private static async Task SearchProductsAsync(IProductFacade productFacade)
        {
            Console.Write("Sök (namn eller kategori): ");
            var searchTerm = Console.ReadLine();

            var results = await productFacade.SearchProductsAsync(searchTerm);

            if (!results.Any())
            {
                Console.WriteLine("Inga produkter matchade sökningen.");
                return;
            }

            foreach (var product in results)
            {
                DisplayProduct(product);
            }
        }

        private static void DisplayProduct(Product product)
        {
            Console.WriteLine($"\nID: {product.Id}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");
            Console.WriteLine($"Lager: {product.Stock}");
            Console.WriteLine($"Kategori: {product.Category}");
            Console.WriteLine($"Skapad: {product.Created}");
            if (product.LastUpdated.HasValue)
                Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
            Console.WriteLine(new string('-', 40));
        }
    }
}