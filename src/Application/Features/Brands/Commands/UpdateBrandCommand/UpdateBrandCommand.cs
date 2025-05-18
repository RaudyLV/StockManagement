
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Commands.UpdateBrandCommand
{
    public class UpdateBrandCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isAvailable { get; set; }
    }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Response<Guid>>
    {
        private readonly IBrandService _service;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IBrandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _service.GetBrandById(request.Id);

            var updatedBrand = _mapper.Map(request, brand);

            await _service.UpdateAsync(updatedBrand);

            return new Response<Guid>(request.Id, "Marca actualizada correctamente!");
        }
    }
}