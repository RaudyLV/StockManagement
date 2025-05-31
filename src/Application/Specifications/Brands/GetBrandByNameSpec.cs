using Application.DTOs;
using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Brands
{
    public class GetBrandByNameSpec : Specification<Brand, BrandDto>
    {
        public GetBrandByNameSpec(string name)
        {
            Query.Where(b => b.Name.ToLower() == name.ToLower())
                 .Select(b => new BrandDto
                 {
                     Id = b.Id,
                     Name = b.Name,
                     Available = b.IsAvailable,
                     Description = b.Description,
                     Products = b.Products.Select(x => new ProductDto
                     {
                         Id = x.Id,
                         BrandName = b.Name,
                         Name = x.Name,
                         Description = x.Description,
                         UnitPrice = x.UnitPrice,
                        Quantity = x.Quantity
                     }).ToList(),
                 });
        }        
    }
}