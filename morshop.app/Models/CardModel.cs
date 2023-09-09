namespace morshop.app.Models
{
    public class CardModel
    {
        public int CardId { get; set; }
        public List<CardItemModel> CardItems { get; set; }
        public double TotalPrice()
        {
            return (double)CardItems.Sum(i=>i.Price);
        }
    }
    public class CardItemModel
    {
        public int CardItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

    }
}