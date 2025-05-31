

using Application.Interfaces;
using Core.Domain.Entities;

namespace Infraestructure.Persistence.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IRepositoryAsync<InventoryMovements> _repo; 

        public InventoryMovementService(IRepositoryAsync<InventoryMovements> repo)
        {
            _repo = repo;
        }


        public async Task AddProductMovementAsync(Product product)
        {
            var movement = new InventoryMovements
            {
                ProductId = product.Id,
                Change = product.Quantity,
                Type =  "In",
                Reason = "Compra",
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(movement);
            await _repo.SaveChangesAsync();
        }
        public async Task AddInventoryUpdateMovement(Product product, int originalQuantity)
        {
            int change = product.Quantity - originalQuantity;

            var movement = new InventoryMovements
            {
                ProductId = product.Id,
                Change = change,
                Type = change > 0 ? "In" : "Out",
                Reason = change > 0 ? "Compra" : "Venta",
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(movement);
            await _repo.SaveChangesAsync();
        }

        public Task<ICollection<InventoryMovements>> GetAllMovementsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InventoryMovements> GetMovementsByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}