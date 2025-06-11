
using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries
{
    public class GetAllBrandsQuery : IRequest<PagedResponse<List<BrandDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string brandName { get; set; }
    }

    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, PagedResponse<List<BrandDto>>>
    {
        private readonly IBrandService _brandService;
        public GetAllBrandsQueryHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<PagedResponse<List<BrandDto>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetBrandsAsync(request);

            return new PagedResponse<List<BrandDto>>(brands, request.PageNumber, request.PageSize);
        }
    }
}