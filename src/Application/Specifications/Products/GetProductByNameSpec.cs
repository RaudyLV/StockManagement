

using Application.DTOs;
using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Products
{
    public class GetProductByNameSpec : Specification<Product, ProductDto>
    {
        public GetProductByNameSpec(string name)
        {
            Query.Where(p => p.Name == name)
                 .Select(p => new ProductDto
                 {
                     Id = p.Id,
                     BrandName = p.Brand!.Name,
                     Name = p.Name,
                     Description = p.Description,
                     UnitPrice = p.UnitPrice,
                     Quantity = p.Quantity
                 });
        }
    }
}