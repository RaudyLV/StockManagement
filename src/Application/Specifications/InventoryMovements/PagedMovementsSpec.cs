

using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Movements
{
    public class PagedMovementsSpec : Specification<InventoryMovements>
    {
        public PagedMovementsSpec(int pageSize, int pageNumber, string reason,
         string productName)
        {
            Query.Include(x => x.Product)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize);

            Query.Where(x =>
            (string.IsNullOrEmpty(reason) || x.Reason.Contains(reason)) &&
            (string.IsNullOrEmpty(productName) || x.Product.Name.Contains(productName))
    );
        }
    }
}