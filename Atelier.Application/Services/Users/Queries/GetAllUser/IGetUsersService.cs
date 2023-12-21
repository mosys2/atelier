using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Queries.GetAllUser
{
    public interface IGetUsersService
    {
        Task<ResultDto<ResultUserDto>> Execute(string? searchKey, int page, int pagesize, string? roleId);
    }
    public class GetUsersService : IGetUsersService
    {
        private readonly IDatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public GetUsersService(IDatabaseContext context,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _context= context;
            _roleManager= roleManager;
            _userManager = userManager;
        }
        public async Task<ResultDto<ResultUserDto>> Execute(string? searchKey, int page, int pagesize,string? roleId)
        {
            var users = _userManager.Users.Include(e => e.Branch).AsQueryable();
            int totalRow = 0;
            if(!string.IsNullOrEmpty(roleId))
            {
                var roleName = await _roleManager.FindByIdAsync(roleId);
                if(roleName == null)
                {
                    return new ResultDto<ResultUserDto>
                    {
                        IsSuccess = false,
                        Data = new ResultUserDto { },
                        Message = Messages.NotFind,
                    };
                }
                    var usersInRole = await _userManager.GetUsersInRoleAsync(roleName.Name);
                    var userIdsInRole = usersInRole.Select(u => u.Id).ToList();
                    users = users
                        .Where(u => userIdsInRole.Contains(u.Id))
                        .AsQueryable();
                return new ResultDto<ResultUserDto>
                {
                    IsSuccess=true,
                    Data=new ResultUserDto
                    {
                        Users=users.Select(e => new GetUsersDto
                        {
                            FullName=e.FirstName+" "+e.LastName,
                            BranchCode=e.Branch.Code,
                            BranchTitle=e.Branch.Title,
                            IsActive=e.IsActive,
                            PhoneNumber = e.PhoneNumber
                        }).ToList().ToPaged(page, pagesize, out totalRow).ToList().OrderByDescending(e => e.InsertTime).ToList(),
                        TotalRow=totalRow
                    }
                };
            }
            return new ResultDto<ResultUserDto>
            {
                IsSuccess = true,
                Data = new ResultUserDto
                {
                    Users = users.Select(e => new GetUsersDto
                    {
                        UserId=e.Id,
                        FullName = e.FirstName + " " + e.LastName,
                        BranchCode = e.Branch.Code,
                        BranchTitle = e.Branch.Title,
                        IsActive = e.IsActive,
                        InsertTime=e.InsertTime,
                        PhoneNumber = e.PhoneNumber
                    }).ToList().ToPaged(page, pagesize, out totalRow).OrderByDescending(e=>e.InsertTime).ToList(),
                    TotalRow = totalRow
                }
            };
        }
    }
}
