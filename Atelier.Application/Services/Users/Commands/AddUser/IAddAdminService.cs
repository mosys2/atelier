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

namespace Atelier.Application.Services.Users.Commands.AddUser
{
    public interface IAddAdminService
    {
        Task<ResultDto> Execute(AddAdminDto admin);
    }
    public class AddAdminService : IAddAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDatabaseContext _context;
        public AddAdminService(UserManager<User> userManager, IDatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ResultDto> Execute(AddAdminDto addAdmin)
        {
            string[] roles = { RoleesName.Admin };

            var branch = await _context.Branches.FindAsync(addAdmin.BranchId);
            if (branch == null) { return new ResultDto { IsSuccess = false, Message = Messages.NoExistBranch }; }
            User user = new User()
            {
                Id=Guid.NewGuid().ToString(),
                FirstName = addAdmin.FirstName,
                LastName = addAdmin.LastName,
                FullName=addAdmin.FirstName+" "+addAdmin.LastName,
                Email = addAdmin.Email,
                Gender = addAdmin.Gender,
                BranchId = branch.Id,
                HomeNumber = addAdmin.HomeNumber,
                InsertTime = DateTime.Now,
                IsActive = addAdmin.IsActive,
                PhoneNumber = addAdmin.PhoneNumber,
                UserName = addAdmin.UserName,
            };
            var result = await _userManager.CreateAsync(user, addAdmin.Password);
            var UserInRole = _userManager.AddToRolesAsync(user, roles).Result;

            if (result.Succeeded && UserInRole.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Messages.RegisterSuccess
                };
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            return new ResultDto
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
