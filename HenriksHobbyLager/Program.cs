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
                repository = factory.CreateRepository(repositoryType, 5.0f);
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