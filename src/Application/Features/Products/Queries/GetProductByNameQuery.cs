using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries
{
    public class GetProductByNameQuery : IRequest<Response<ProductDto>>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Response<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public GetProductByNameQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Response<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productService.GetProductByNameAsync(request.Name);
            if (existingProduct == null)
                throw new NotFoundException("Producto no encontrado!");

            var product = _mapper.Map<ProductDto>(existingProduct);

            return new Response<ProductDto>(product);
        }
    }
}