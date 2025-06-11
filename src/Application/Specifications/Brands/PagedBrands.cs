

using Application.DTOs;
using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Brands
{
    public class PagedBrandsSpec : Specification<Brand, BrandDto>
    {
        public PagedBrandsSpec(int pageSize, int pageNumber, string brandName)
        {
            Query.Include(x => x.Products)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .Select(p => new BrandDto
            {
                Id = p.Id,
                Name = p.Name,
                Available = p.IsAvailable,
                Description = p.Description,
                Products = p.Products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    BrandName = p.Name,
                    Description = x.Description,
                    IsAvailable = x.IsAvailable,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity
                }).ToList()
            });
            

            if (!string.IsNullOrEmpty(brandName))
                Query.Search(x => x.Name, "%" + brandName + "%");
        }
    }
}