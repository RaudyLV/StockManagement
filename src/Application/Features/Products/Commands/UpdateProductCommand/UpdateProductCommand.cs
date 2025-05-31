
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        public UpdateProductCommandHandler(IMapper mapper, IProductService productService, IBrandService brandService)
        {
            _mapper = mapper;
            _productService = productService;
            _brandService = brandService;
        }

        public async Task<Response<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(request.Id);
            if (product == null)
                throw new NotFoundException("La marca no fue encontrada!");

            var brand = await _brandService.GetBrandById(request.BrandId);
            if (brand == null)
                throw new NotFoundException("La marca no fue encontrada!");

            var updatedProduct = _mapper.Map(request, product);

            await _productService.UpdateAsync(updatedProduct);

            return new Response<Guid>(request.Id, "Producto actualizado correctamente");

        }
    }
}