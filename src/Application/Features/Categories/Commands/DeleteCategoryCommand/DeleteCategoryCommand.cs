
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<Guid>>
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public DeleteCategoryCommandHandler(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            await _service.DeleteAsync(category);

            return new Response<Guid>(request.Id, "Categoria eliminada exitosamente!");
        }
    }
}