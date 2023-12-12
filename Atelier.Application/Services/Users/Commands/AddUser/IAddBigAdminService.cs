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
        Task<ResultDto> Execute();
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

        public async Task<ResultDto> Execute()
        {
            string branchId = "2AA8546F-04B1-49B3-9B14-1DE0DC57DA3D";
            string phoneNumber = "09136836864";
            string password = "123456";
            string[] roles = [RoleesName.BigAdmin];
            

            var branch = await _context.Branches.FindAsync(branchId);
            if (branch == null){ return new ResultDto { IsSuccess=false, Message="!شعبه یافت نشد" }; }

            
            User user = new User()
            {
                FirstName="محمد باقر ",
                LastName="شاهمیر",
                Email="mohammadsaadati117@gmail.com",
                Gender=1,
                BranchId=branch.Id,
                HomeNumber="03155672760",
                InsertTime=DateTime.Now,
                IsActive=true,
                PhoneNumber="09136836864",
                UserName="mbshah",
            };
            var result = await _userManager.CreateAsync(user, password);
            var UserInRole = _userManager.AddToRolesAsync(user, roles).Result;
            
            if (result.Succeeded && UserInRole.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.RegisterSuccess
                };
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message+=item.Description + Environment.NewLine;
            }
            return new ResultDto
            {
                IsSuccess = false,
                Message=message
            };


        }
    }
}
