using Microsoft.AspNetCore.Mvc;
using morshop.app.Components;
using morshop.business.Abstract;
using morshop.entity;

namespace morshop.app.Controllers
{
    public class HomeController:Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService=productService;
            _categoryService=categoryService;
        }
        public IActionResult Index()
        {
            var indexCopmponent = new IndexComponent
            {
                DiscountProducts=_productService.ListDiscountProducts(),
                Top10Products=_productService.GetTop10Products()
            };

            return View("Index",indexCopmponent);
        }
        public IActionResult Categories()
        {
            var Categories = _categoryService.GetAll();
            return View("Categories",Categories);
        }
    }
}