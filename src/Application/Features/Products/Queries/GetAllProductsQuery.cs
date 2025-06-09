

using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<PagedResponse<List<ProductDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<List<ProductDto>>>
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _service.GetProductsAsync(request);

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return new PagedResponse<List<ProductDto>>(productsDto, request.PageNumber, request.PageSize);
        }
    }
}