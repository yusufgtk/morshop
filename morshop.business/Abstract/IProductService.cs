using morshop.entity;

namespace morshop.business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Task<List<Product>> GeTAllAsync();
        void Create(Product entity);
        Task CreateAsync(Product entity);
        void Update(Product entity);
        Task UpdateAsync(Product entity);
        void Delete(Product entity);
        Task DeleteeAsync(Product entity);
        Product GetProductById(int id);
        Task<Product> GetProductByIdAsync(int id);
        Product GetProductByUrl(string url);
        List<Product> GetTop10Products();
        List<Product> ListProducts();
        List<Product> ListDiscountProducts();
        List<Product> GetProductsByCategoryId(int id);
        List<Product> GetProductsBySearch(string search);
    }
}