namespace CatalogAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}