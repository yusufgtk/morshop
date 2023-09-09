namespace morshop.entity
{
    public class Card
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CardItem> CardItems { get; set; }
    }
}