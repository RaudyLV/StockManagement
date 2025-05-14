
namespace Core.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; set; }
        //M:1 entre productos y sus marcas
        public List<Product> Products { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; } 
    }
}