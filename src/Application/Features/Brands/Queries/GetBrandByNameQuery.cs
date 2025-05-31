
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries
{
    public class GetBrandByNameQuery : IRequest<Response<BrandDto>>
    {
        public string Name { get; set; } 
    }

    public class GetBrandByNameQueryHandler : IRequestHandler<GetBrandByNameQuery, Response<BrandDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;

        public GetBrandByNameQueryHandler(IMapper mapper, IBrandService brandService)
        {

            _mapper = mapper;
            _brandService = brandService;
        }

        public async Task<Response<BrandDto>> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {

            var existingBrand = await _brandService.GetBrandByNameAsync(request.Name);
            if (existingBrand == null)
                throw new NotFoundException("La marca no fue encontrada!");

            var brand = _mapper.Map<BrandDto>(existingBrand);

            return new Response<BrandDto>(brand);
        }       
    }
}