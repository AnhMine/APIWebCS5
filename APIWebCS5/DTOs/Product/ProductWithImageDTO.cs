namespace APIWebCS5.DTOs.Product
{
    public class ProductWithImageDTO
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string Hair { get; set; }
        public string StatusHair { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public bool Sex { get; set; }
        public string Popular { get; set; }
        public byte Stock { get; set; }
        public string? Description { get; set; }
        public int IdCategory { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
