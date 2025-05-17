

using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Response<CategoryDto>>
    {
        public Guid Id { get; set; }
    

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<CategoryDto>>
        {
            private readonly ICategoryService _service;
            private readonly IMapper _mapper;
            public GetCategoryByIdQueryHandler(ICategoryService service, IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }

            public async Task<Response<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _service.GetByIdAsync(request.Id);

                var categoryDto = _mapper.Map<CategoryDto>(category);

                return new Response<CategoryDto>(categoryDto);
            }
        }
    }
}