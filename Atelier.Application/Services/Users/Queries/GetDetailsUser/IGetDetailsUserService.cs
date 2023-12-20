using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Queries.GetDetailsUser
{
    public interface IGetDetailsUserService
    {
        Task<ResultDto<GetDetailUserDto>> Execute(string userId);
    }
    public class GetDetailsUserService : IGetDetailsUserService
    {
        private readonly IDatabaseContext _context;
        public GetDetailsUserService(IDatabaseContext context)
        {
            _context = context;   
        }
        public async Task<ResultDto<GetDetailUserDto>> Execute(string userId)
        {
            var user =await _context.Users.Include(t=>t.Branch).Where(u=>u.Id==userId).FirstOrDefaultAsync();
            if(user == null)
            {
                return new ResultDto<GetDetailUserDto>
                { 
                IsSuccess= false,
                Data=new GetDetailUserDto{ },
                Message=Messages.NotFind,
                };
            }
            return new ResultDto<GetDetailUserDto>
            {
                IsSuccess = true,
                Data = new GetDetailUserDto
                {
                    Address = user.Address,
                    BirthDay = user.BirthDay,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FirstName + " " + user.LastName,
                    Gender = user.Gender,
                    HomeNumber = user.HomeNumber,
                    IsActive = user.IsActive,
                    ProfileImage = user.ProfileImage,
                    BranchTitle = user.Branch.Title,
                },
            };
        }
    }
}
