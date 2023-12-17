using Atelier.Application.Interfaces.Contexts;
using Atelier.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.SaveToken
{
    public interface ISaveUserTokenService
    {
        void Execute(JwtUserToken jwtUserToken);
    }
    public class SaveUserTokenService : ISaveUserTokenService
    {
        private readonly IDatabaseContext _context;
        public SaveUserTokenService(IDatabaseContext context)
        {
            _context = context;
        }
        public  void Execute(JwtUserToken jwtUserToken)
        {
            try
            {
                _context.JwtUserTokens.Add(jwtUserToken);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }
           
        }
    }
}
