using System.ComponentModel.DataAnnotations;

namespace morshop.app.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email Formatında olması gerekiyor!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}