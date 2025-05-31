
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;

using MediatR;

namespace Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<Response<Guid>>
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Guid>>
    {
        private readonly IProductService _service;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductService service, IMapper mapper, IBrandService brandService)
        {
            _service = service;
            _mapper = mapper;
            _brandService = brandService;
        }

        public async Task<Response<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _service.GetProductByNameAsync(request.Name);
            if (existingProduct != null)
                throw new AlreadyExistException("El producto que intenta agregar ya existe!");
                
            var brand = await _brandService.GetBrandById(request.BrandId);
            if (brand == null)
                throw new NotFoundException("La marca no fue encontrada!");

            var newProduct = _mapper.Map<Product>(request);

            await _service.AddAsync(newProduct);

            return new Response<Guid>(newProduct.Id, "Producto creado correctamente.");
        }
    }
}