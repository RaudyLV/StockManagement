

using Application.DTOs;
using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Products
{
    public class PagedProductsSpec : Specification<Product, ProductDto>
    {
       public PagedProductsSpec(int pageSize, int pageNumber, string name,
                         string brandName, double price, int quantity)
        {
            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                BrandName = p.Brand!.Name,
                Name = p.Name,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Quantity = p.Quantity,
                IsAvailable = p.IsAvailable,
            });

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