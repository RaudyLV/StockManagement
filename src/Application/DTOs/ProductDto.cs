

using Core.Domain.Entities;

namespace Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
    }
}