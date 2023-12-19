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
    public interface IEditCustomerService
    {
        Task<ResultDto> Execute(string id, string editByUserId, EditCustomerDto editCustomer);
    }
    public class EditCustomerService : IEditCustomerService
    {
        private readonly IDatabaseContext _context;
        public EditCustomerService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id, string editByUserId, EditCustomerDto editCustomer)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.MessageNotfindUser
                };
            }
            user.UserName = editCustomer.UserName;
            user.Address = editCustomer.Address;
            user.PhoneNumber = editCustomer.PhoneNumber;
            user.Email = editCustomer.Email;
            user.BirthDay = editCustomer.BirthDay;
            user.FirstName = editCustomer.FirstName;
            user.LastName = editCustomer.LastName;
            user.FullName = editCustomer.FirstName + " " + editCustomer.LastName;
            user.Gender = editCustomer.Gender;
            user.IsActive = editCustomer.IsActive;
            user.HomeNumber = editCustomer.HomeNumber;
            user.UpdateTime = DateTime.Now;
            user.BranchId = editCustomer.BranchId;
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
