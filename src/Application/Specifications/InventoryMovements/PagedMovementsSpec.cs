

using Application.DTOs;
using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Movements
{
    public class PagedMovementsSpec : Specification<InventoryMovements, InventoryMovementsDto>
    {
        public PagedMovementsSpec(int pageSize, int pageNumber, string reason,
         string productName)
        {
            Query.Include(x => x.Product)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .Select(m => new InventoryMovementsDto
            {
                ProductName = m.Product.Name,
                Change = m.Change,
                Type = m.Type,
                Reason = m.Reason,
                Timestamp = m.Timestamp
            });

            Query.Where(x =>
            (string.IsNullOrEmpty(reason) || x.Reason.Contains(reason)) &&
            (string.IsNullOrEmpty(productName) || x.Product.Name.Contains(productName))
    );
        }
    }
}