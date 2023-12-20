using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Roles.Queries.GetRoles
{
    public interface IGetRolesService
    {
        Task<List<GetAllRoleDto>> Execute();
    }
    public class GetRolesService : IGetRolesService
    {
        private readonly RoleManager<Role> _roleManager;

        public GetRolesService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<GetAllRoleDto>> Execute()
        {
            var result = await _roleManager.Roles
                 .Where(r => r.IsRemoved == false)
                .OrderByDescending(i => i.InsertTime)
                .Select(r => new GetAllRoleDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name,
                    PersianTitle = r.PersianTitle
                })
                .ToListAsync();
            return result;
        }
    }
}
