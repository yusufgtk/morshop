using System.ComponentModel.DataAnnotations;

namespace  morshop.app.Models
{
    public class RoleEditModel
    {
        public string Id { get; set; }
        
        [Required(ErrorMessage ="Zorunlu alan")]
        public string Name { get; set; }

    }
}