

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.Users;
using Core.Domain.Settings;
using Infraestructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.Identity.Helpers
{
    public class JWTHelper
    {
        private readonly JWTSettings _jWTSettings;
        private readonly UserManager<AppUser> _userManager;

        public JWTHelper(IOptions<JWTSettings> jWTSettings, UserManager<AppUser> userManager)
        {
            _jWTSettings = jWTSettings.Value;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> GetTokenAsync(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleList = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleList.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            string ip = ipHelper.GetIp();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("ip", ip),
                new Claim("uid", user.Id)
            }.Union(userClaims).Union(roleList);

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));

            var signInCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var JwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jWTSettings.Expires),
                signingCredentials: signInCredentials
            );

            return JwtSecurityToken;
        }


        public RefreshToken RefreshToken(string ip)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                ExpireTime = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ip
            };
        }

        //Genera un string de 64 bits para el token
        private string RandomTokenString()
        {
            var randomBytes = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return BitConverter.ToString(randomBytes)
                 .Replace("+", "") // Elimina '+' para mas legibilidad
                .Replace("/", "_") // reemplaza '/' y '+' para la URL
                .TrimEnd('='); //
        }
    }
}