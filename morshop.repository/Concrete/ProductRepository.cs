using Microsoft.EntityFrameworkCore;
using morshop.entity;
using morshop.repository.Abstract;


namespace morshop.repository.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context):base(context)
        {
            
        }
        private ShopContext ShopContext
        {
            get{return context as ShopContext;}
        }
        public Product GetProductById(int id)
        {
            return ShopContext.Products.FirstOrDefault(i=>i.Id==id);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await ShopContext.Products.FirstOrDefaultAsync(i=>i.Id==id);
        }

        public Product GetProductByUrl(string url)
        {

            return ShopContext.Products.FirstOrDefault(i=>i.Url==url);

        }

        public List<Product> GetProductsByCategoryId(int id)
        {
            return ShopContext.Products.Where(i=>(i.CategoryId==id)&&(i.IsApproved==true)).ToList();
        }

        public List<Product> GetProductsBySearch(string search)
        {
            return ShopContext.Products.Where(i=>i.IsApproved&&(i.Name.ToLower().Contains(search.ToLower())||i.Description.ToLower().Contains(search.ToLower()))).ToList();
        }

        public List<Product> GetTop10Products()
        {
            return ShopContext.Products.OrderByDescending(i=>i.NumberOfSales).Skip(0).Take(10).ToList();
        }

        public List<Product> ListDiscountProducts()
        {
            return ShopContext.Products.Where(i=>(i.PreviousPrice-i.CurrentPrice)>0).ToList();
        }

        public List<Product> ListProducts()
        {
            return ShopContext.Products.Where(i => i.IsApproved == true).ToList();
        }
        
    }
}