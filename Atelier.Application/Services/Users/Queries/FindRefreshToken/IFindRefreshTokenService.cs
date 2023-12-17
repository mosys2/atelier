using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Common.Helpers;
using Atelier.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Queries.FindRefreshToken
{
    public interface IFindRefreshTokenService
    {
        Task<ResultDto<JwtUserToken>> Execute(string refreshToken);
    }
    public class FindRefreshTokenService : IFindRefreshTokenService
    {
        private readonly IDatabaseContext _context;
        public FindRefreshTokenService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<JwtUserToken>> Execute(string refreshToken)
        {
            SecurityHelper securityHelper = new SecurityHelper();

            string RefreshTokenHash = securityHelper.Getsha256Hash(refreshToken);
            var usertoken = _context.JwtUserTokens.Include(p => p.User)
                .SingleOrDefault(p => p.RefreshToken == RefreshTokenHash);
            if (usertoken == null)
            {
                return new ResultDto<JwtUserToken>
                {

                    IsSuccess = false,
                    Message = Messages.NotFoundRefreshToken,
                };
            }
            if (usertoken.RefreshTokenExp < DateTime.Now)
            {
                return new ResultDto<JwtUserToken>
                {

                    IsSuccess = false,
                    Message = Messages.NotFoundRefreshToken
                };
            }
            return new ResultDto<JwtUserToken>
            {
                IsSuccess=true,
                Message=Messages.ValidRefreshToken,
                Data=usertoken,
            };
        }
    }
}
