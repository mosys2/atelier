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
    public interface IAddSecretaryService
    {
        Task<ResultDto> Execute(AddSecretaryDto secretary);
    }
    public class AddSecretaryService : IAddSecretaryService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDatabaseContext _context;
        public AddSecretaryService(UserManager<User> userManager, IDatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ResultDto> Execute(AddSecretaryDto secretary)
        {
            string[] roles = { RoleesName.Secretary };
            var branch = await _context.Branches.FindAsync(secretary.BranchId);
            if (branch == null) { return new ResultDto { IsSuccess = false, Message = Messages.NoExistBranch }; }
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = secretary.FirstName,
                LastName = secretary.LastName,
                FullName = secretary.FirstName + " " + secretary.LastName,
                Email = secretary.Email,
                Gender = secretary.Gender,
                BranchId = branch.Id,
                HomeNumber = secretary.HomeNumber,
                InsertTime = DateTime.Now,
                IsActive = secretary.IsActive,
                PhoneNumber = secretary.PhoneNumber,
                UserName = secretary.PhoneNumber + "_" + branch.Code,
            };
            var result = await _userManager.CreateAsync(user, secretary.Password);
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
