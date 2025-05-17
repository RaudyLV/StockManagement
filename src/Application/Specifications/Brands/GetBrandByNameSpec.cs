using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Brands
{
    public class GetBrandByNameSpec : Specification<Brand>
    {
        public GetBrandByNameSpec(string name)
        {
            Query.Where(b => b.Name != name);
        }        
    }
}