namespace TechTest.DTO
{
    public class ProductsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public string? BarCode { get; set; }

        public bool IsValid()
        {
            return Price>0 && Quantity > 0;
        }
    }
}