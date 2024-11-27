/*
/*
    HENRIKS HOBBYLAGER™ 1.0
    Skapat av: Henrik Hobbykodare
    Datum: En sen kväll i oktober efter fyra Red Bull
    Version: 1.0 (eller kanske 1.1, jag har ändrat lite sen första versionen)

    TODO-lista:
    * Lägga till stöd för bilder på produkterna (kanske)
    * Göra backups (förlorade nästan allt förra veckan när skärmsläckaren startade)
    * Kolla upp det där med "molnet" som alla pratar om
    * Lägg till ljudeffekter när man lägger till produkter???
    * Fixa så att programmet startar automatiskt när datorn startar om
    * Be någon förklara vad "dependency injection" betyder
    * Köpa en UPS (strömavbrott är INTE kul!)
    * Lära mig vad XML är (folk säger att det är viktigt)
    * Göra en logga till programmet i Paint
*/

using HenriksHobbyLager.Facades;
using HenriksHobbyLager.Factories;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.UI;
namespace HenriksHobbyLager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose the storage type: 'sqlite' or 'mongodb'");
            string repositoryType = Console.ReadLine()?.ToLower() ?? "sqlite";

            var factory = new ProductRepositoryFactory();
            IRepository<Product> repository;

            try
            {
                repository = factory.CreateRepository(repositoryType);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }

            var productFacade = new ProductFacade(repository);
            var consoleMenuHandler = new ConsoleMenuHandler();

            ConsoleMenuHandler.RunMenuAsync(productFacade).Wait();
        }
    }
}