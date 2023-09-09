using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using morshop.app.Identity;

namespace morshop.app.Models
{
    public class RoleModel
    {
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string Name { get; set; }
    }

}