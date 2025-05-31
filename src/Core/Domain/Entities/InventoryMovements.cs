
namespace Core.Domain.Entities
{
    public class InventoryMovements
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public int Change { get; set; } // positivo: entrada, negativo: salida de inventario
        public string Type { get; set; } = "In"; // "In" o "Out"
        public string Reason { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; } //Varuable de Navegacion
    }
}