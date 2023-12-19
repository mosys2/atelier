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
    public interface IEditSecretaryService
    {
        Task<ResultDto> Execute(string id, string editByUserId, EditSecretaryDto editSecretary);
    }
    public class EditSecretaryService : IEditSecretaryService
    {
        private readonly IDatabaseContext _context;
        public EditSecretaryService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id, string editByUserId, EditSecretaryDto editSecretary)
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
            user.UserName = editSecretary.UserName;
            user.Address = editSecretary.Address;
            user.PhoneNumber = editSecretary.PhoneNumber;
            user.Email = editSecretary.Email;
            user.BirthDay = editSecretary.BirthDay;
            user.FirstName = editSecretary.FirstName;
            user.LastName = editSecretary.LastName;
            user.FullName = editSecretary.FirstName + " " + editSecretary.LastName;
            user.Gender = editSecretary.Gender;
            user.IsActive = editSecretary.IsActive;
            user.HomeNumber = editSecretary.HomeNumber;
            user.UpdateTime = DateTime.Now;
            user.BranchId = editSecretary.BranchId;
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
