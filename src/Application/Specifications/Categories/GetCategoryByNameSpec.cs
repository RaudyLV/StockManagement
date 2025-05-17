
using Ardalis.Specification;
using Core.Domain.Entities;

namespace Application.Specifications.Categories
{
    public class GetCategoryByNameSpec : Specification<Category>
    {
        public GetCategoryByNameSpec(string name)
        {
            Query.Where(c => c.Name != name);
        }
    }
}