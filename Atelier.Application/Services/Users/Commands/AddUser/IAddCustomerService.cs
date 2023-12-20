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
    public interface IAddCustomerService
    {
        Task<ResultDto> Execute(AddCustomerDto customer);
    }
    public class AddCustomerService : IAddCustomerService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDatabaseContext _context;

        public AddCustomerService(UserManager<User> userManager, IDatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ResultDto> Execute(AddCustomerDto customer)
        {
            string[] roles = { RoleesName.Customer };
            var branch = await _context.Branches.FindAsync(customer.BranchId);
            if (branch == null) { return new ResultDto { IsSuccess = false, Message = Messages.NoExistBranch }; }
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                FullName = customer.FirstName + " " + customer.LastName,
                Email = customer.Email,
                Gender = customer.Gender,
                BranchId = branch.Id,
                HomeNumber = customer.HomeNumber,
                InsertTime = DateTime.Now,
                IsActive = customer.IsActive,
                PhoneNumber = customer.PhoneNumber,
                UserName = customer.PhoneNumber + "_" + branch.Code,
            };
            var result = await _userManager.CreateAsync(user, customer.Password);
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
