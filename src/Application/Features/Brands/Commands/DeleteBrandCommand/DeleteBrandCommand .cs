

using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrandCommand
{
    public class DeleteBrandCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Response<Guid>>
    {
        private readonly IBrandService _service;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request);

            await _service.DeleteAsync(brand);

            return new Response<Guid>(request.Id, "Marca eliminada exitosamente!");
        }
    }
}