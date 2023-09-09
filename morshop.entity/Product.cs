namespace morshop.entity
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public int NumberOfSales { get; set; }

    }
}