using System.ComponentModel.DataAnnotations;
using morshop.entity;

namespace morshop.app.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string City { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        public CardModel CardModel { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }


        //ödeme bilgileri
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public string ExpirationMonth { get; set; }
        [Required(ErrorMessage ="Zorunlu alan!")]
        public string ExpirationYears { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!")]
        public int Cvc { get; set; }

        public string PaymentId { get; set; }
        public string ConversationId { get; set; }

    }
}