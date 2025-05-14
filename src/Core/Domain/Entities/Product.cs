

namespace Core.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        //1:1
        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }

        // un producto puede tener una o mas categorias.
        public List<Category> Categories { get; set; } 

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; } 
        public int Quantity { get; set; }
    }
}