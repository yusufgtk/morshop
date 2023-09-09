using System.ComponentModel.DataAnnotations;

namespace morshop.app.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Boş bırakılamaz!")]
        [MaxLength(20,ErrorMessage ="Max 20 karakterden oluşabilir!")]
        [MinLength(3,ErrorMessage ="Min 3 karakterden oluşabilir!")]
        public string? Name { get; set; }
        public string? Url { get; set; }

        //public List<ProductCategory>? ProductCategory { get; set; }

    }
}