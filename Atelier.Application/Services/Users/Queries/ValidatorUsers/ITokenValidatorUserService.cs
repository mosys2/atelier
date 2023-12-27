using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Services.Users.Commands.CheckToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Queries.ValidatorUsers
{
    public interface ITokenValidatorUserService
    {
        Task Execute(TokenValidatedContext context);
    }
    public class TokenValidatorUserService : ITokenValidatorUserService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly ICheckTokenUserService _checkTokenUserService;
        public TokenValidatorUserService(IDatabaseContext databaseContext,ICheckTokenUserService checkTokenUserService)
        {
            _databaseContext = databaseContext;
            _checkTokenUserService= checkTokenUserService;
        }
        public async Task Execute(TokenValidatedContext context)
        {
            var claimsidentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsidentity?.Claims == null || !claimsidentity.Claims.Any())
            {
                context.Fail("claims not found....");
                return;
            }
            var userId = claimsidentity.FindFirst(e=>e.Type==ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId.Value))
            {
                context.Fail("claims not found....");
                return;
            }

            var user =await _databaseContext.Users.FindAsync(userId.Value);

            if (user?.IsActive == false)
            {
                context.Fail("User not Active");
                return;
            }
            if (!(context.SecurityToken is JwtSecurityToken Token)
                || !_checkTokenUserService.Execute(Token.RawData))
            {
                context.Fail("توکن در دیتابیس وجود ندارد");
                return;
            }
        }
    }
}
