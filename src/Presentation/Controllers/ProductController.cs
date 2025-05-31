
using Application.Features.Products.Commands.CreateProductCommand;
using Application.Features.Products.Commands.DeleteProductCommand;
using Application.Features.Products.Commands.UpdateProductCommand;
using Application.Features.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseApiController
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            return Ok(await Mediator!.Send(new GetProductByNameQuery { Name = name }));
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok(await Mediator!.Send(new DeleteProductCommand { Id = id }));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( Guid id, [FromBody] UpdateProductCommand request)
        {
            return Ok(await Mediator!.Send(new UpdateProductCommand
            {
                Id = id,
                BrandId = request.BrandId,
                Name = request.Name,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity,
            }));
        }
    }
}