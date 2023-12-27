using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Services.Roles.Queries.GetRolesUser;
using Atelier.Application.Services.Users.Commands.SaveToken;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Common.Helpers;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Auth
{
    public interface ILoginService
    {
        Task<ResultDto<ResultLoginDto>> Execute(RequestLoginDto request);
    }
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDatabaseContext _context;
        private readonly IConfiguration configuration;
        private readonly ISaveUserTokenService _saveUserTokenService;
        private readonly IGetRolesUserService _getRolesUserService;
        public LoginService(UserManager<User> userManager,
            ISaveUserTokenService saveUserTokenService,
            IGetRolesUserService getRolesUserService,
            IDatabaseContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _saveUserTokenService = saveUserTokenService;
            _getRolesUserService= getRolesUserService;
            _context = context;
            this.configuration = configuration;
        }
        public async Task<ResultDto<ResultLoginDto>> Execute(RequestLoginDto request)
        {
            SecurityHelper securityHelper = new SecurityHelper();
            var checkUser = _userManager.Users
         .Include(u => u.Branch)
         .Where(u => u.Branch.Code == request.BranchCode && u.PhoneNumber == request.UserName).FirstOrDefault();
            if (checkUser == null)
            {
                return new ResultDto<ResultLoginDto>()
                {
                    Message=Messages.MessageNotFind,
                    IsSuccess=false
                };
            }
            var checkPassword = await _userManager.CheckPasswordAsync(checkUser, request.Password);
            if (!checkPassword)
            {
                return new ResultDto<ResultLoginDto>
                {
                    IsSuccess = false,
                    Message = Messages.MessageInvalidPassword
                };
            }
            var claims = await _userManager.GetClaimsAsync(checkUser);
            var claimUser = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier, checkUser.Id.ToString()),
                    new Claim ("BranchId", checkUser.BranchId.ToString()),
                };
            await _userManager.AddClaimsAsync(checkUser, claimUser);

            //Add Role To Claim
            var roles = await _getRolesUserService.Execute(checkUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string key = configuration["JWtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddDays(int.Parse(configuration["JWtConfig:expires"]));
            var token = new JwtSecurityToken(
                  issuer: configuration["JWtConfig:issuer"],
                  audience: configuration["JWtConfig:audience"],
                  expires: tokenexp,
                  notBefore: DateTime.Now,
                  claims: claims,

                  signingCredentials: credentials
                  );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();
            _saveUserTokenService.Execute(new JwtUserToken()
            {
                Id=Guid.NewGuid().ToString(),
                TokenExp = tokenexp,
                TokenHash = securityHelper.Getsha256Hash(jwtToken),
                RefreshToken = securityHelper.Getsha256Hash(refreshToken),
                RefreshTokenExp = DateTime.Now.AddDays(3),
                UserId = checkUser.Id,
            });
            return new ResultDto<ResultLoginDto>
            {
                IsSuccess=true,
                Message=Messages.LoginSucceed,
                Data=new ResultLoginDto
                {
                    JwtToken=jwtToken,
                    RefreshJwtToken=refreshToken,
                },
                Id= checkUser.Id
            };
        }
    }
}