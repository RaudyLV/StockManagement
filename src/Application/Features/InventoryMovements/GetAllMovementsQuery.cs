using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.InventoryMovements
{
    public class GetAllMovementsQuery : IRequest<PagedResponse<List<InventoryMovementsDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
    }

    public class GetAllMovementsQueryHandler : IRequestHandler<GetAllMovementsQuery, PagedResponse<List<InventoryMovementsDto>>>
    {
        private readonly IInventoryMovementService _movementService;
        private readonly IMapper _mapper;
public GetAllMovementsQueryHandler(IInventoryMovementService movementService, IMapper mapper)
        {
            _movementService = movementService;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<InventoryMovementsDto>>> Handle(GetAllMovementsQuery request, CancellationToken cancellationToken)
        {
            var movements = await _movementService.GetAllMovementsAsync(request);

            var movementsDto = _mapper.Map<List<InventoryMovementsDto>>(movements);

            return new PagedResponse<List<InventoryMovementsDto>>(movementsDto, request.PageNumber, request.PageSize);
        }
    }
}