
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<Guid>>
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _service.GetByIdAsync(request.Id);

            var updatedCategory = _mapper.Map(request, category);

            await _service.UpdateAsync(updatedCategory);

            return new Response<Guid>(request.Id, "Categoria actulizada correctamente");
        }
    }

}