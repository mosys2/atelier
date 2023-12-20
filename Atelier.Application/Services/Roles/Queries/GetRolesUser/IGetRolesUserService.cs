using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Roles.Queries.GetRolesUser
{
    public interface IGetRolesUserService
    {
        Task<IList<string>> Execute(User user);
    }
    public class GetRolesUserService : IGetRolesUserService
    {
        private readonly UserManager<User> _userManager;
        public GetRolesUserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IList<string>> Execute(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
    }
}
