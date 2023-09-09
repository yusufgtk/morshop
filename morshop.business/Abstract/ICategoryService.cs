using morshop.entity;

namespace morshop.business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        Category GetCategoryByUrl(string url);
        Category GetCategoryById(int id);

    }
}