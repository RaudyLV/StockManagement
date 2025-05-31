

using Core.Domain.Entities;

namespace Application.Interfaces
{
    public interface IInventoryMovementService
    {
        Task<InventoryMovements> GetMovementsByIdAsync(Guid Id);
        Task<ICollection<InventoryMovements>> GetAllMovementsAsync();
        Task AddProductMovementAsync(Product product);
        Task AddInventoryUpdateMovement(Product product, int originalQuantity);
    }
}