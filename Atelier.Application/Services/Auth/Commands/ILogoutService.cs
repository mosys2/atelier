using Atelier.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Auth.Commands
{
    public interface ILogoutService
    {
        void Execute(string userId);
    }
    public class LogoutService : ILogoutService
    {
        private readonly IDatabaseContext _context;
        public LogoutService(IDatabaseContext context)
        {
            _context = context;
        }
        public void Execute(string userId)
        {
            var userToken=_context.JwtUserTokens.Where(u=>u.UserId==userId).ToList();
                _context.JwtUserTokens.RemoveRange(userToken);
                _context.SaveChanges();
        }
    }
}
