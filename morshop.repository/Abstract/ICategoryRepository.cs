using morshop.entity;

namespace morshop.repository.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByUrl(string url);
        Category GetCategoryById(int id);
    }
}