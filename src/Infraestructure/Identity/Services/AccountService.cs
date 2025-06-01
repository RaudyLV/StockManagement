
using System.IdentityModel.Tokens.Jwt;
using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Core.Domain.Entities;
using Core.Domain.Settings;
using Infraestructure.Identity.Helpers;
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly JWTHelper _jWTHelper;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
         IUserService userService, SignInManager<AppUser> signInManager, JWTHelper jWTHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _signInManager = signInManager;
            _jWTHelper = jWTHelper;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
                throw new NotFoundException("El email no fue encontrado.");

            var result = await _signInManager.PasswordSignInAsync(user.Email!, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new NotFoundException("La contraseña o email son incorrectos");

            JwtSecurityToken securityToken = await _jWTHelper.GetTokenAsync(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            response.Email = user.Email!;
            response.UserName = user.UserName!;

            var roleList = await _userManager.GetRolesAsync(user);
            response.Roles = roleList.ToList();
            response.IsAuthenticated = user.EmailConfirmed;

            return new Response<AuthenticationResponse>(response, message: "Logeo completado exitosamente!");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var existingEmail = await _userManager.FindByNameAsync(request.Email);
            if (existingEmail != null)
                throw new AlreadyExistException("El email ingresado ya existe!");

            var identityUser = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded)
            {
                var domainUser = new DomainUser
                {
                    Id = identityUser.Id,
                    FullName = request.FullName
                };

                await _userService.AddAsync(domainUser);

                await _userManager.AddToRoleAsync(identityUser, Roles.User.ToString());
                return new Response<string>(identityUser.Id, message: "Usuario creado correctamente!");
            }
            else
            {
                return new Response<string>(result.Errors.Select(e => e.Description).FirstOrDefault()!);
            }
        }
    }
}