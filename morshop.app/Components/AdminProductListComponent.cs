using morshop.entity;

namespace morshop.app.Components
{
    public class AdminProductListComponent
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public string? Message { get; set; }
        

    }
}