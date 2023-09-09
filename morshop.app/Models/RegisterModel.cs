using System.ComponentModel.DataAnnotations;

namespace morshop.app.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string FirstName { get; set; }


        [Required(ErrorMessage ="Zorunlu alan!")]
        public string LastName { get; set; }


        [Required(ErrorMessage ="Zorunlu alan!")]
        public string UserName { get; set; }


        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler aynı değil!")]
        public string RePassword { get; set; }


        [Required(ErrorMessage ="Zorunlu alan!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}