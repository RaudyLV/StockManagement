
namespace Application.DTOs
{
    public class InventoryMovementsDto
    {
        public string ProductName { get; set; } 
        public int Change { get; set; } // positivo: entrada, negativo: salida de inventario
        public string Type { get; set; }  // "In" o "Out"
        public string Reason { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}