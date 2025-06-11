using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
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
        public GetAllMovementsQueryHandler(IInventoryMovementService movementService)
        {
            _movementService = movementService;
        }

        public async Task<PagedResponse<List<InventoryMovementsDto>>> Handle(GetAllMovementsQuery request, CancellationToken cancellationToken)
        {
            var movements = await _movementService.GetAllMovementsAsync(request);

            return new PagedResponse<List<InventoryMovementsDto>>(movements, request.PageNumber, request.PageSize);
        }
    }
}