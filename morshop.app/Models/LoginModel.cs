using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace morshop.app.Models
{
    public class LoginModel
    {
        // [Required(ErrorMessage = "Zorunlu alan!")]
        // public string UserName { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!564654")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

    }
}