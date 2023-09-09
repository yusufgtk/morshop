namespace morshop.repository.Abstract
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        Task<List<T>> GeTAllAsync();


        void Create(T entity);
        Task CreateAsync(T entity);


        void Update(T entity);
        Task UpdateAsync(T entity);


        void Delete(T entity);
        Task DeleteeAsync(T entity);

    }
}