using morshop.business.Abstract;
using morshop.entity;
using morshop.repository.Abstract;

namespace morshop.business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }
        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public async Task CreateAsync(Product entity)
        {
            await _productRepository.CreateAsync(entity);
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public async Task DeleteeAsync(Product entity)
        {
            await _productRepository.DeleteeAsync(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public async Task<List<Product>> GeTAllAsync()
        {
            return await _productRepository.GeTAllAsync();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public Product GetProductByUrl(string url)
        {
            return _productRepository.GetProductByUrl(url);
        }

        public List<Product> GetProductsByCategoryId(int id)
        {
            return _productRepository.GetProductsByCategoryId(id);
        }

        public List<Product> GetProductsBySearch(string search)
        {
            return _productRepository.GetProductsBySearch(search);
        }

        public List<Product> GetTop10Products()
        {
            return _productRepository.GetTop10Products();
        }

        public List<Product> ListDiscountProducts()
        {
            return _productRepository.ListDiscountProducts();
        }

        public List<Product> ListProducts()
        {
            return _productRepository.ListProducts();
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await _productRepository.UpdateAsync(entity);
        }
    }
}