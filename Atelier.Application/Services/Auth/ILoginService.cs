using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        public LoginService(UserManager<User> userManager, IDatabaseContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
           this.configuration = configuration;
        }
        public async Task<ResultDto<ResultLoginDto>> Execute(RequestLoginDto request)
        {
            var findUser =await _userManager.FindByNameAsync(request.UserName);
            if(findUser == null)
            {
                return new ResultDto<ResultLoginDto>()
                {
                Message=Messages.MessageNotFind,
                IsSuccess=false
                };
            }
         
            var branchCode = _userManager.Users
           .Include(u => u.Branch) 
           .Where(u => u.Branch.Code == request.BranchCode&&u.UserName==findUser.UserName)
           .FirstOrDefaultAsync()?.Result?.Branch.Code;

            if (branchCode!=request.BranchCode)
            {
                return new ResultDto<ResultLoginDto>
                {
                    IsSuccess=false,
                    Message=Messages.NoExistBranchForUser
                };
            }
            var checkPassword =await _userManager.CheckPasswordAsync(findUser,request.Password);
            if(!checkPassword)
            {
                return new ResultDto<ResultLoginDto>
                {
                    IsSuccess = false,
                    Message = Messages.MessageInvalidPassword
                };
            }
            var claims =await _userManager.GetClaimsAsync(findUser);
            string key = configuration["JWtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                  issuer: configuration["JWtConfig:issuer"],
                  audience: configuration["JWtConfig:audience"],
                  expires: DateTime.Now.AddMinutes(int.Parse(configuration["JWtConfig:expires"])),
                  notBefore: DateTime.Now,
                  claims: claims,
                  signingCredentials: credentials
                  );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new ResultDto<ResultLoginDto>
            {
                IsSuccess=true,
                Message=Messages.LoginSucceed,
                Data=new ResultLoginDto
                {
                JwtToken=jwtToken
                },
                Id=findUser.Id
            };
        }
    }
}
