
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;

using MediatR;

namespace Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<Response<Guid>>
    {
        public Guid CategoryId{ get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double UnitPrice { get; set; } 
        public int Quantity { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repo;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IRepositoryAsync<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);

            await _repo.AddAsync(newProduct);
            await _repo.SaveChangesAsync();

            return new Response<Guid>(newProduct.Id, "Producto creado correctamente.");
        }
    }
}