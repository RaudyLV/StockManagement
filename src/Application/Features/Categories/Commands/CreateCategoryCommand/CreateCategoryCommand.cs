
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isAvailable { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;
        public CreateCategoryCommandHandler(IMapper mapper, ICategoryService service)
        {

            _mapper = mapper;
            _service = service;
        }

        public async Task<Response<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            await _service.AddAsync(category);

            return new Response<Guid>(category.Id, "Categoria creada exitosamente!");
        }
    }
}