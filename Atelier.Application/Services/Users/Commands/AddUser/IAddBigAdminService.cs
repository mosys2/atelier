using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Atelier.Domain.Entities.Users;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.AddUser
{
    public interface IAddBigAdminService
    {
        Task<ResultDto> Execute(AddBigAdminDto bigAdmin);
    }
    public class AddBigAdminService : IAddBigAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDatabaseContext _context;
        public AddBigAdminService(UserManager<User> userManager, IDatabaseContext context)
        {
            _userManager=userManager;
            _context=context;
        }

        public async Task<ResultDto> Execute(AddBigAdminDto bigAdmin)
        {
            string[] roles = { RoleesName.Customer };
            var branch = await _context.Branches.FindAsync(bigAdmin.BranchId);
            if (branch == null) { return new ResultDto { IsSuccess = false, Message = Messages.NoExistBranch }; }
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = bigAdmin.FirstName,
                LastName = bigAdmin.LastName,
                FullName = bigAdmin.FirstName + " " + bigAdmin.LastName,
                Email = bigAdmin.Email,
                Gender = bigAdmin.Gender,
                BranchId = branch.Id,
                HomeNumber = bigAdmin.HomeNumber,
                InsertTime = DateTime.Now,
                IsActive = bigAdmin.IsActive,
                PhoneNumber = bigAdmin.PhoneNumber,
                UserName = bigAdmin.PhoneNumber + "_" + branch.Code,
            };
            var result = await _userManager.CreateAsync(user, bigAdmin.Password);
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
