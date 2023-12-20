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
    public interface IEditEmployeeService
    {
        Task<ResultDto> Execute(string id, string editByUserId, EditEmployeeDto editEmployee);
    }
    public class EditEmployeeService : IEditEmployeeService
    {
        private readonly IDatabaseContext _context;
        public EditEmployeeService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id, string editByUserId, EditEmployeeDto editEmployee)
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
            user.UserName = editEmployee.PhoneNumber + "_" + user.Branch.Code;
            user.Address = editEmployee.Address;
            user.PhoneNumber = editEmployee.PhoneNumber;
            user.Email = editEmployee.Email;
            user.BirthDay = editEmployee.BirthDay;
            user.FirstName = editEmployee.FirstName;
            user.LastName = editEmployee.LastName;
            user.FullName = editEmployee.FirstName + " " + editEmployee.LastName;
            user.Gender = editEmployee.Gender;
            user.IsActive = editEmployee.IsActive;
            user.HomeNumber = editEmployee.HomeNumber;
            user.UpdateTime = DateTime.Now;
            user.BranchId = editEmployee.BranchId;
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
