using Atelier.Application.Interfaces.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
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
        public TokenValidatorUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task Execute(TokenValidatedContext context)
        {
            var claimsidentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsidentity?.Claims == null || !claimsidentity.Claims.Any())
            {
                context.Fail("claims not found....");
                return;
            }
            var userId = claimsidentity.FindFirst("UserId").Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Fail("claims not found....");
                return;
            }

            var user =await _databaseContext.Users.FindAsync(userId);

            if (user?.IsActive == false)
            {
                context.Fail("User not Active");
                return;
            }
        }
    }
}
