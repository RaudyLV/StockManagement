using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Products.Queries
{
    public class GetProductByNameQuery : IRequest<Response<ProductDto>>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Response<ProductDto>>
    {
        private readonly IProductService _productService;

        public GetProductByNameQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Response<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productService.GetProductByNameAsync(request.Name);
            if (existingProduct == null)
                throw new NotFoundException("Producto no encontrado!");


            return new Response<ProductDto>(existingProduct);
        }
    }
}