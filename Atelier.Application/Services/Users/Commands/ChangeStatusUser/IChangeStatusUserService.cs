using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.ChangeStatusUser
{
    public interface IChangeStatusUserService
    {
        Task<ResultDto> Execute(string  userId);
    }
    public class ChangeStatusUserService : IChangeStatusUserService
    {
        private readonly IDatabaseContext _context;
        public ChangeStatusUserService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                };
            }
            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.ChangeStatusUser
            };
        }
    }
}
