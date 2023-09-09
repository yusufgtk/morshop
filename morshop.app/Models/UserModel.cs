using System.ComponentModel.DataAnnotations;

namespace morshop.app.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public string EmailAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}