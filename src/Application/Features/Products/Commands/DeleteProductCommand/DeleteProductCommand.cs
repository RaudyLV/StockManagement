using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHanlder : IRequestHandler<DeleteProductCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public DeleteProductCommandHanlder(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<Response<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productService.GetProductByIdAsync(request.Id);
            if (existingProduct == null)
                throw new NotFoundException("Producto no encontrado!");

            var product = _mapper.Map<Product>(request);

            await _productService.DeleteAsync(product); 

            return new Response<Guid>(product.Id, "Producto eliminado correctamente");
        }
    }
}