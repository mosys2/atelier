using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.EditUser
{
    public interface IEditAdminService
    {
        Task<ResultDto> Execute(string id, string editByUserId, EditAdminDto editAdmin);
    }
    public class EditAdminService : IEditAdminService
    {
        private readonly IDatabaseContext _context;
        public EditAdminService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id,string editByUserId, EditAdminDto editAdmin)
        {
            var user =await _context.Users.FindAsync(id);
            if(user==null)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=Messages.MessageNotfindUser
                };
            }
            user.UserName=editAdmin.UserName;
            user.Address=editAdmin.Address;
            user.PhoneNumber=editAdmin.PhoneNumber;
            user.Email=editAdmin.Email;
            user.BirthDay=editAdmin.BirthDay;
            user.FirstName=editAdmin.FirstName;
            user.LastName=editAdmin.LastName;
            user.FullName=editAdmin.FirstName+" "+editAdmin.LastName;
            user.Gender=editAdmin.Gender;
            user.IsActive=editAdmin.IsActive;
            user.HomeNumber=editAdmin.HomeNumber;
            user.UpdateTime = DateTime.Now;
            user.BranchId=editAdmin.BranchId;
            user.UpdateByUserId = editByUserId;
           await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess=true,
                Message=Messages.MessageUpdate,
            };
        }
    }
}
