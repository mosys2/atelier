using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.EditUser
{
    public interface IEditBigAdminService
    {
        Task<ResultDto> Execute(string id, string editByUserId, EditBigAdminDto editBigAdmin);
    }
    public class EditBigAdminService : IEditBigAdminService
    {
        private readonly IDatabaseContext _context;
        public EditBigAdminService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id, string editByUserId, EditBigAdminDto editBigAdmin)
        {
            var user = await _context.Users.Include(b => b.Branch).Where(i => i.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.MessageNotfindUser
                };
            }
            user.UserName = editBigAdmin.PhoneNumber + "_" + user.Branch.Code;
            user.Address = editBigAdmin.Address;
            user.PhoneNumber = editBigAdmin.PhoneNumber;
            user.Email = editBigAdmin.Email;
            user.BirthDay = editBigAdmin.BirthDay;
            user.FirstName = editBigAdmin.FirstName;
            user.LastName = editBigAdmin.LastName;
            user.FullName = editBigAdmin.FirstName + " " + editBigAdmin.LastName;
            user.Gender = editBigAdmin.Gender;
            user.IsActive = editBigAdmin.IsActive;
            user.HomeNumber = editBigAdmin.HomeNumber;
            user.UpdateTime = DateTime.Now;
            user.BranchId = editBigAdmin.BranchId;
            user.UpdateByUserId = editByUserId;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.MessageUpdate,
            };
        }
    }

}
