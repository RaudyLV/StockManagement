

using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Products
{
    public class PagedProductsSpec : Specification<Product>
    {
       public PagedProductsSpec(int pageSize, int pageNumber, string name,
                         string brandName, double price, int quantity)
        {
            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(brandName))
                Query.Search(x => x.Brand!.Name, "%" + brandName + "%");

            if (!string.IsNullOrEmpty(name))
                Query.Search(x => x.Name, "%" + name + "%");

            if (price > 0)
                Query.Where(x => x.UnitPrice == price);

            if (quantity > 0)
                Query.Where(x => x.Quantity == quantity);
        }

    }
}