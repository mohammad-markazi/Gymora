using Gymora.Database;
using Gymora.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User
{
    public class AuthService(IVerifyCodeService verifyCodeService,IHttpContextAccessor httpContextAccessor,IGymoraDbContext context):IAuthService
    {
        public async Task SendVerifyCode(string phoneNumber, CancellationToken cancellationToken)
        {
            if (!verifyCodeService.CheckExistsSendVerifyCode(phoneNumber))
            {
                var code = await verifyCodeService.SendVerifyCode(phoneNumber);
                await context.VerifyCodes.AddAsync(new VerifyCodeModel()
                {
                    PhoneNumber = phoneNumber,
                    Code = code,
                    IsActive = true,
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public string GenerateToken(UserModel userModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
                new Claim(ClaimTypes.Name, userModel.Username),
                new Claim("FullName", userModel.FullName),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your_Secret_Key_Here_this_is_program_sport_for_gymora"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken("www.gymora.net", "http://localhost:36145/", claims,
                expires: DateTime.Now.AddDays(10), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }

        public int GetCurrentUserId()
        {
            var user = httpContextAccessor.HttpContext?.User;
            return int.Parse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public string GetCurrentUserFullName()
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user?.FindFirst("FullName")?.Value;
        }

        public string GetCurrentUsername()
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user?.Identity?.Name;
        }
    }
}
