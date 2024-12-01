namespace HenriksHobbyLager.Interfaces
{
    internal interface IRepositoryFactory<T>
    {
        IRepository<T> CreateRepository(string type);
    }
}
