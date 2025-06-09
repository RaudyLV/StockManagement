using Application.Features.InventoryMovements;
using Application.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementsController : BaseApiController
    {
        [HttpGet()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllMovementsParameters query)
        {
            return Ok(await Mediator!.Send(new GetAllMovementsQuery
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                ProductName = query.ProductName,
                Reason = query.Reason,
            }));
        }
    }
}