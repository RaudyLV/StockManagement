using Application.Features.Brands.Commands.CreateBrandCommand;
using Application.Features.Brands.Commands.UpdateBrandCommand;
using Application.Features.Brands.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : BaseApiController
    {
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            return Ok(await Mediator!.Send(new GetBrandByNameQuery { Name = name }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBrandCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, UpdateBrandCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }
    }
}