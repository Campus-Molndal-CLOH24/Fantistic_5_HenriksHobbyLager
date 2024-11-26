using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.UI
{
    internal class ConsoleMenuHandler
    {
        public static void RunMenu(IProductFacade productFacade)
        {
            // Huvudloopen - Stäng inte av programmet, då försvinner allt!
            while (true)
            {
                Console.Clear();  // Rensar skärmen så det ser proffsigt ut
                Console.WriteLine("=== Henriks HobbyLager™ 1.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök produkter");
                Console.WriteLine("6. Avsluta");  // Använd inte denna om du vill behålla datan!

                var choice = Console.ReadLine();

                // Switch är tydligen bättre än if-else enligt Google
                switch (choice)
                {
                    case "1":
                        ShowAllProducts(productFacade);
                        break;
                    case "2":
                        AddProduct(productFacade);
                        break;
                    case "3":
                        UpdateProduct(productFacade);
                        break;
                    case "4":
                        DeleteProduct(productFacade);
                        break;
                    case "5":
                        SearchProducts(productFacade);
                        break;
                    case "6":
                        return;  //Avslutar programmet
                    default:
                        Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
                Console.ReadKey();
            }
        }

        // Visar alla produkter som finns i "databasen"
        private static void ShowAllProducts(IProductFacade productFacade)
        {
            // Kollar om det finns några produkter alls
            // !_products.Any() låter mer proffsigt än _products.Count == 0
            if (!productFacade.GetAllProducts().Any())
            {
                Console.WriteLine("Inga produkter finns i lagret. Dags att shoppa grossist!");
                return;
            }

            foreach (var product in productFacade.GetAllProducts())
            {
                DisplayProduct(product);
            }
        }

        // Lägger till en ny produkt i systemet
        private static void AddProduct(IProductFacade productFacade)
        {
            Console.WriteLine("=== Lägg till ny produkt ===");

            Console.Write("Namn: ");
            var name = Console.ReadLine();

            Console.Write("Pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Ogiltigt pris! Använd punkt istället för komma (lärde mig den hårda vägen)");
                return;
            }

            Console.Write("Antal i lager: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Ogiltig lagermängd! Hela tal endast (kan inte sälja halva helikoptrar)");
                return;
            }

            Console.Write("Kategori: ");
            var category = Console.ReadLine();

            // Skapar produkten - Id räknas upp automatiskt så vi slipper hålla reda på det
            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category,
                Created = DateTime.Now  // Automatiskt datum, smooth!
            };

            productFacade.CreateProduct(product);
            Console.WriteLine("Produkt tillagd! Glöm inte att hålla datorn igång!");
        }

        // Uppdaterar en befintlig produkt
        private static void UpdateProduct(IProductFacade productFacade)
        {
            Console.Write("Ange produkt-ID att uppdatera (finns i listan ovan): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror tack!");
                return;
            }

            // LINQ - Google säger att det är snabbt
            var product = productFacade.GetProduct(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte! Är du säker på att du skrev rätt?");
                return;
            }

            // Uppdatera bara det som användaren faktiskt skriver in
            Console.Write("Nytt namn (tryck bara enter om du vill behålla det gamla): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                product.Name = name;

            Console.Write("Nytt pris (tryck bara enter om du vill behålla det gamla): ");
            var priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price))
                product.Price = price;

            Console.Write("Ny lagermängd (tryck bara enter om du vill behålla den gamla): ");
            var stockInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stockInput) && int.TryParse(stockInput, out int stock))
                product.Stock = stock;

            Console.Write("Ny kategori (tryck bara enter om du vill behålla den gamla): ");
            var category = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(category))
                product.Category = category;

            product.LastUpdated = DateTime.Now;  // Håller koll på när saker ändras
            Console.WriteLine("Produkt uppdaterad! Stäng fortfarande inte av datorn!");

            productFacade.UpdateProduct(product);
        }

        // Ta bort en produkt (använd med försiktighet!)
        private static void DeleteProduct(IProductFacade productFacade)
        {
            Console.Write("Ange produkt-ID att ta bort (dubbel-check att det är rätt, går inte att ångra!): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror är tillåtna här.");
                return;
            }

            var product = productFacade.GetProduct(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte! Puh, inget blev raderat av misstag!");
                return;
            }

            productFacade.DeleteProduct(product.Id);
            Console.WriteLine("Produkt borttagen! (Hoppas det var meningen)");
        }

        // Sökfunktion - Min stolthet! Söker i både namn och kategori
        private static void SearchProducts(IProductFacade productFacade)
        {
            Console.Write("Sök (namn eller kategori - versaler spelar ingen roll!): ");
            var searchTerm = Console.ReadLine().ToLower();

            var results = productFacade.SearchProducts(searchTerm);

            if (!results.Any())
            {
                Console.WriteLine("Inga produkter matchade sökningen. Prova med något annat!");
                return;
            }

            foreach (var product in results)
            {
                DisplayProduct(product);
            }
        }

        private static void DisplayProduct(Product product)
        {
            // Snygga streck som separerar produkterna
            Console.WriteLine($"\nID: {product.Id}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");  // :C gör att det blir kronor automatiskt!
            Console.WriteLine($"Lager: {product.Stock}");
            Console.WriteLine($"Kategori: {product.Category}");
            Console.WriteLine($"Skapad: {product.Created}");
            if (product.LastUpdated.HasValue)  // Kollar om produkten har uppdaterats någon gång
                Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
            Console.WriteLine(new string('-', 40));  // Snyggt streck mellan produkterna
        }


    }
}
