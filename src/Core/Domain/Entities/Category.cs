
namespace Core.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; }
        
    }
}