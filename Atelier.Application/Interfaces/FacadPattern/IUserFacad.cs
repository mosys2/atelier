using Atelier.Application.Services.Auth.Commands;
using Atelier.Application.Services.Auth;
using Atelier.Application.Services.Users.Commands.ChangeStatusUser;
using Atelier.Application.Services.Users.Queries.FindRefreshToken;
using Atelier.Application.Services.Users.Queries.GetAllUser;
using Atelier.Application.Services.Users.Queries.GetDetailsUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atelier.Application.Services.Users.Commands.AddPageAccess;
using Atelier.Application.Services.Users.Queries.GetUserPageAccess;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IUserFacad
    {
       IGetUsersService GetUsersService { get; }
       IGetDetailsUserService GetDetailsUserService { get; }
       IChangeStatusUserService ChangeStatusUserService { get; }
       ILoginService LoginService { get; }
       IFindRefreshTokenService FindRefreshTokenService { get; }
       ILogoutService LogoutService { get; }
        IAddPageAccessService AddPageAccessService { get; }
        IGetAllUserPageAccessService GetAllUserPageAccessService { get; }
    }
}
