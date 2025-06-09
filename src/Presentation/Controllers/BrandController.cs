using Application.Features.Brands.Commands.CreateBrandCommand;
using Application.Features.Brands.Commands.UpdateBrandCommand;
using Application.Features.Brands.Queries;
using Application.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : BaseApiController
    {

        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBrandsAsync([FromQuery] GetAllBrandsParameters query)
        {
           return Ok(await Mediator!.Send(new GetAllBrandsQuery
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                brandName = query.brandName,
            }));
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            return Ok(await Mediator!.Send(new GetBrandByNameQuery { Name = name }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, UpdateBrandCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }
    }
}