using morshop.entity;
using morshop.repository.Concrete;

namespace morshop.repository.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
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