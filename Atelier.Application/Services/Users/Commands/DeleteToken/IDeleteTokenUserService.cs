using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.DeleteToken
{
    public interface IDeleteTokenUserService
    {
        void Execute(string RefreshToken);
    }
    public class DeleteTokenUserService : IDeleteTokenUserService
    {
        private readonly IDatabaseContext _context;
        public DeleteTokenUserService(IDatabaseContext context)
        {
            _context = context;
        }
        public async void Execute(string RefreshToken)
        {
            SecurityHelper securityHelper = new SecurityHelper();
            string RefreshTokenHash = securityHelper.Getsha256Hash(RefreshToken);
            var token =_context.JwtUserTokens.Where(r=>r.RefreshToken== RefreshTokenHash).FirstOrDefault();
            if (token != null)
            {
                _context.JwtUserTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }
    }
}
