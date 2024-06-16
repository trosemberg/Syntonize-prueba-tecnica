using Microsoft.EntityFrameworkCore;

namespace TechTestData.Models
{
    public class Products : TEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public string? BarCode { get; set; }
    }
}