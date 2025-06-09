
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
        private readonly IMapper _mapper;
        public GetAllBrandsQueryHandler(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<BrandDto>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetBrandsAsync(request);

            var brandsDto = _mapper.Map<List<BrandDto>>(brands);

            return new PagedResponse<List<BrandDto>>(brandsDto, request.PageNumber, request.PageSize);
        }
    }
}