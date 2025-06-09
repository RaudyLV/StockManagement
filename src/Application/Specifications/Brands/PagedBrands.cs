

using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Brands
{
    public class PagedBrandsSpec : Specification<Brand>
    {
        public PagedBrandsSpec(int pageSize, int pageNumber, string brandName)
        {
            Query.Include(x => x.Products)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(brandName))
                Query.Search(x => x.Name, "%" + brandName + "%");
        }
    }
}