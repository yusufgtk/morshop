using morshop.business.Abstract;
using morshop.entity;
using morshop.repository.Abstract;

namespace morshop.business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }
        public void Create(Category entity)
        {
            _categoryRepository.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public Category GetCategoryByUrl(string url)
        {
            return _categoryRepository.GetCategoryByUrl(url);
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity);
        }
    }
}