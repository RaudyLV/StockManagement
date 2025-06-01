
using Application.DTOs.Users;
using Application.Features.Auth.Commands.AuthenticationCommand;
using Application.Features.Auth.Commands.RegisterCommand;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await Mediator!.Send(new AuthenticationCommand
            {
                Email = request.Email,
                Password = request.Password,
                ipAddress = GetIpAddress()
            }));
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] RegisterRequest request)
        {
            return Ok(await Mediator!.Send(new RegisterCommand
            {
                FullName = request.FullName,
                Email = request.Email,  
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers["Origin"].FirstOrDefault() ?? string.Empty
            }));
        }

        //Metodo auxiliar para obtener el origen de ip del usuario accediendo y haciendo la request.
        //Se utiliza para el jwt en los Helpers.
        private string GetIpAddress()
        {
            if (Request.Headers.TryGetValue("X-Forwarded-For", out var forwarderIp) && !string.IsNullOrWhiteSpace(forwarderIp))
            {
                return forwarderIp.ToString();
            }

            var remoteIp = Request.HttpContext.Connection.RemoteIpAddress;
            return remoteIp != null ? remoteIp.MapToIPv4().ToString() : "Ip no valida";
        }
    }
}