
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrandCommand
{
    public class CreateBrandCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; } 
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _service;
        public CreateBrandCommandHandler(IMapper mapper, IBrandService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<Response<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request);

            await _service.AddAsync(brand); 

            return new Response<Guid>(brand.Id, "Marca creada exitosamente!");
        }
    }
}