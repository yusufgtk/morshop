using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using morshop.entity;
using morshop.repository.Abstract;

namespace morshop.repository.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext context):base(context)
        {
            
        }
        private ShopContext ShopContext
        {
            get{return context as ShopContext;}
        }
        public Category GetCategoryById(int id)
        {
            return ShopContext.Categories.FirstOrDefault(i=>i.Id==id);
                
        }

        public Category GetCategoryByUrl(string url)
        {
            return ShopContext.Categories.Where(i=>i.Url==url).FirstOrDefault();
        }
    }

}