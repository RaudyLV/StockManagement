using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Categories Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; } 
        public int Quantity { get; set; }
    }
}