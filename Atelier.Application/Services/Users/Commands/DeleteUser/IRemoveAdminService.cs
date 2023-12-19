using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.DeleteUser
{
    public interface IRemoveAdminService
    {
        Task<ResultDto> Execute(string UserId, string RemoveByUserId);

    }
    public class RemoveAdminService : IRemoveAdminService
    {
        private readonly IDatabaseContext _context;
        private readonly UserManager<User> _userManager;
        public RemoveAdminService(IDatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ResultDto> Execute(string UserId,string RemoveByUserId)
        {
            var checkUserId = await _context.Users.FindAsync(UserId);
            if (checkUserId == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.MessageNotfindUser
                };
            }
            var roleUser = await _userManager.GetRolesAsync(checkUserId);
            bool isAdmin = false;
            foreach (var role in roleUser)
            {
                if (role == RoleesName.Admin)
                {
                    isAdmin = true;
                }
            }
            if (!isAdmin)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NoExistRoleAdminInUser
                };
            }
            checkUserId.RemoveByUserId = RemoveByUserId;
            checkUserId.IsRemoved = true;
            checkUserId.RemoveTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.Delete
            };
        }
    }
}
