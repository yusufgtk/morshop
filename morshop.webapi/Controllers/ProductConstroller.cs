using morshop.business.Abstract;
using morshop.business.Concrete;
using Microsoft.AspNetCore.Mvc;
using morshop.entity;

namespace morshop.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService=productService;
        }
        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {
            var products= await _productService.GeTAllAsync();
            var productsDTO = new List<ProductDTO>();
            foreach(var p in products)
            {
                var productDTO = new ProductDTO(){
                    Id=p.Id,
                    CategoryId=p.CategoryId,
                    Name=p.Name,
                    Description=p.Description,
                    CurrentPrice=p.CurrentPrice,
                    PreviousPrice=p.PreviousPrice,
                    ImageUrl=p.ImageUrl,
                    Url=p.Url
                };
                productsDTO.Add(productDTO);
            }
            return Ok(productsDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id);
            var productDTO=new ProductDTO(){
                Id=product.Id,
                CategoryId=product.CategoryId,
                Name=product.Name,
                Description=product.Description,
                CurrentPrice=product.CurrentPrice,
                PreviousPrice=product.PreviousPrice,
                ImageUrl=product.ImageUrl,
                Url=product.Url
            };
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product entity)
        {
            await _productService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetProduct),new {id=entity.Id});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product entity)
        {
            if(id==null)
            {
                return BadRequest();
            }
            var product = await _productService.GetProductByIdAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            product.Name=entity.Name;
            product.CategoryId=entity.CategoryId;
            product.Description=entity.Description;
            product.CurrentPrice=entity.CurrentPrice;
            product.ImageUrl=entity.ImageUrl;
            product.IsApproved=entity.IsApproved;
            product.IsHome=entity.IsHome;
            await _productService.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id==null)
            {
                return BadRequest();
            }
            var product = await _productService.GetProductByIdAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            await _productService.DeleteeAsync(product);
            return NoContent();
        }
    }
}