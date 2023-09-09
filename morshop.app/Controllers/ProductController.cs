using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using morshop.app.Components;
using morshop.business.Abstract;
using morshop.entity;


namespace morshop.app.Controllers
{
    public class ProductController:Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService=productService;
            _categoryService=categoryService;
        }
        public IActionResult List(string categoryUrl)
        {
            var listCompanent = new ListComponent();
            if(categoryUrl==null)
            {
                listCompanent.Products = _productService.ListProducts();
                listCompanent.Categories = _categoryService.GetAll();
                listCompanent.Products = listCompanent.Products.OrderBy(a => System.Guid.NewGuid()).ToList();
            }
            else
            {
                var _category = _categoryService.GetCategoryByUrl(categoryUrl);
                if(_category==null)
                {
                    return View("Error");
                }
                listCompanent.Products = _productService.GetProductsByCategoryId(_category.Id);
                listCompanent.Categories = _categoryService.GetAll();
            }
            return View("List",listCompanent);
        }
        public IActionResult Details(string url)
        {
            if(Url==null)
            {
                return View("Error");
            }
            else
            {
                var product = _productService.GetProductByUrl(url);
                if(product==null)
                {
                    return View("Error"); 
                }
                return View("Details",product);
            }
        }
        
        public IActionResult Search(string search)
        {   
            var listComponent = new ListComponent();
            listComponent.Categories=_categoryService.GetAll();
            if(search==null)
            {
                listComponent.Products=new List<Product>();
                return View("List",listComponent);
            }
            else{
                listComponent.Products=_productService.GetProductsBySearch(search);
            }
            return View("List",listComponent);
        }

    }
}