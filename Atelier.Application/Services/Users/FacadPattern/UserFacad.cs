using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Auth;
using Atelier.Application.Services.Auth.Commands;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Application.Services.Roles.Queries.GetRolesUser;
using Atelier.Application.Services.Users.Commands.AddPageAccess;
using Atelier.Application.Services.Users.Commands.ChangeStatusUser;
using Atelier.Application.Services.Users.Commands.SaveToken;
using Atelier.Application.Services.Users.Queries.FindRefreshToken;
using Atelier.Application.Services.Users.Queries.GetAllUser;
using Atelier.Application.Services.Users.Queries.GetDetailsUser;
using Atelier.Application.Services.Users.Queries.GetUserPageAccess;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.FacadPattern
{
    public class UserFacad : IUserFacad
    {
        private readonly IDatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration configuration;
        private readonly ISaveUserTokenService _saveUserTokenService;
        private readonly IGetRolesUserService _getRolesUserService;
        public UserFacad(IDatabaseContext context, UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, ISaveUserTokenService saveUserTokenService, IGetRolesUserService getRolesUserService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _saveUserTokenService = saveUserTokenService;
            _getRolesUserService = getRolesUserService;
            this.configuration = configuration;
        }
        private  IGetUsersService _usersService;
        private  IGetDetailsUserService _detailsUserService;
        private  IChangeStatusUserService _changeStatusUserService;
        private  ILoginService _loginService;
        private  IFindRefreshTokenService _findRefreshTokenService;
        private  ILogoutService _logoutService;
        private  IAddPageAccessService _changePageAccessService;
        private  IGetAllUserPageAccessService _getAllUserPageAccessService;
        public IGetUsersService GetUsersService
        {
            get
            {
                return _usersService = _usersService ?? new GetUsersService(_context,_roleManager,_userManager);
            }
        }

        public IGetDetailsUserService GetDetailsUserService
        {
            get
            {
                return _detailsUserService = _detailsUserService ?? new GetDetailsUserService(_context);
            }
        }

        public IChangeStatusUserService ChangeStatusUserService
        {
            get
            {
                return _changeStatusUserService = _changeStatusUserService ?? new ChangeStatusUserService(_context);
            }
        }

        public IFindRefreshTokenService FindRefreshTokenService
        {
            get
            {
                return _findRefreshTokenService = _findRefreshTokenService ?? new FindRefreshTokenService(_context);
            }
        }

        public ILogoutService LogoutService
        {
            get
            {
                return _logoutService = _logoutService ?? new LogoutService(_context);
            }
        }

        public ILoginService LoginService
        {
            get
            {
                return _loginService = _loginService ?? new LoginService(_userManager,_saveUserTokenService,_getRolesUserService,_context,configuration);
            }
        }

        public IAddPageAccessService AddPageAccessService
        {
            get
            {
                return _changePageAccessService = _changePageAccessService ?? new AddPageAccessService(_context);
            }
        }

        public IGetAllUserPageAccessService GetAllUserPageAccessService
        {
            get
            {
                return _getAllUserPageAccessService = _getAllUserPageAccessService ?? new GetAllUserPageAccessService(_context);
            }
        }
    }
}
