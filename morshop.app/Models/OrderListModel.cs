using morshop.entity;

namespace morshop.app.Models
{
    public class OrderListModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        public string PaymentId { get; set; }
        public string ConversationId { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }

        public EnumOrderState OrderState { get; set; }
        public List<OrderItemModel> OrderItemsModel { get; set; }

        public double TotalPrice()
        {
            return OrderItemsModel.Sum(i=>i.Price*i.Quantity);
        }

    }

    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

    }
}
