using System.ComponentModel.DataAnnotations;

namespace morshop.app.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Lütfen kategori seçiniz!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Lütfen ürün adını giriniz!")]
        [MaxLength(25,ErrorMessage = "Ürün adı en fazla 25 karakterden oluşabilir!")]
        [MinLength(5,ErrorMessage = "Ürün adı en az 5 karakterden oluşabilir!")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Lütfen ürün açıklamasını giriniz!")]
        [MaxLength(200,ErrorMessage = "Ürün açıklaması en fazla 200 karakterden oluşabilir!")]
        [MinLength(20,ErrorMessage = "Ürün açıklaması en az 5 karakterden oluşabilir!")]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Lütfen ürün fiyatını giriniz!")]
        [Range(maximum:1000000,minimum:0,ErrorMessage ="Ürün fiyatı 0-1.000.000 arasında olmalı!")]
        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }

        //[Required(ErrorMessage ="Lütfen ürün fiyatını giriniz!")]
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public int NumberOfSales { get; set; }

        //public List<ProductCategory>? ProductCategory { get; set; } 

    }
}