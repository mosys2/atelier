using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.CheckToken
{
    public interface ICheckTokenUserService
    {
        bool Execute(string token);
    }
    public class CheckTokenUserService : ICheckTokenUserService
    {
        private readonly IDatabaseContext _context;
        public CheckTokenUserService(IDatabaseContext context)
        {
            _context = context;
        }
        public bool Execute(string token)
        {
            SecurityHelper securityHelper = new SecurityHelper();
            string tokenHash = securityHelper.Getsha256Hash(token);
            var userToken = _context.JwtUserTokens.Where(p => p.TokenHash == tokenHash).FirstOrDefault();
            return userToken == null ? false : true;
        }
    }
}
