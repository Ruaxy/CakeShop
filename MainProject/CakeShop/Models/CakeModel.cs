namespace CakeShop.Models
{
    public class CakeModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public long TypeId { get; set; }
        public string OwnerSpecification { get; set; }
        public string ImageUrl { get; set; }

    }
}
