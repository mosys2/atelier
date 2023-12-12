using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Customers.Commands
{
    public interface IAddCustomerService
    {
        Task<ResultDto> Execute(RegisterCustmerDto custmer);
    }
    public class AddCustomerService : IAddCustomerService
    {
        private readonly UserManager<User> _userManager;
        public AddCustomerService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResultDto> Execute(RegisterCustmerDto custmer)
        {
            try
            {
                User user = new User()
                {
                    FirstName=custmer.FirstName,
                    LastName=custmer.LastName,
                    PhoneNumber=custmer.PhoneNumber,
                    Address=custmer.Address,
                    BirthDay = custmer.BirthDay,
                    Gender = custmer.Gender,
                    InsertTime = DateTime.Now,
                };
               var result= await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message=Messages.RegisterSuccess
                    };
                }
                //Error in Register
                string message = "";
                foreach (var item in result.Errors.ToList())
                {
                    message+=item.Description + Environment.NewLine;
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    Message=message
                };

            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message= ex.Message
                };
            }
        }
    }

}
