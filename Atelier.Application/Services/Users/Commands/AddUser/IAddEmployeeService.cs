using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.AddUser
{
    public interface IAddEmployeeService
    {
        Task<ResultDto> Execute(AddEmployeeDto employee);
    }
    public class AddEmployeeService : IAddEmployeeService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDatabaseContext _context;

        public AddEmployeeService(UserManager<User> userManager, IDatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ResultDto> Execute(AddEmployeeDto employee)
        {
            string[] roles = { RoleesName.Employee };
            var branch = await _context.Branches.FindAsync(employee.BranchId);
            if (branch == null) { return new ResultDto { IsSuccess = false, Message = Messages.NoExistBranch }; }
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = employee.FirstName + " " + employee.LastName,
                Email = employee.Email,
                Gender = employee.Gender,
                BranchId = branch.Id,
                HomeNumber = employee.HomeNumber,
                InsertTime = DateTime.Now,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                UserName = employee.PhoneNumber + "_" + branch.Code,
            };
            var result = await _userManager.CreateAsync(user, employee.Password);
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
