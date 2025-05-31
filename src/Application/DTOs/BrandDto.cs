using Core.Domain.Entities;

namespace Application.DTOs
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; } 
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}