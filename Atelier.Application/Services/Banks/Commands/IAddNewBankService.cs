using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Banks.Commands
{
    public interface IAddNewBankService
    {
        Task<ResultDto> Execute(RequestBankDto request, Guid userId, Guid branchId);
    }
    public class AddNewBankService : IAddNewBankService
    {
        private readonly IMongoRepository<Bank> _bankRepository;

        public AddNewBankService(IMongoRepository<Bank> bankRepository)
        {
            _bankRepository = bankRepository;
        }
        public async Task<ResultDto> Execute(RequestBankDto request, Guid userId, Guid branchId)
        {
            Bank bank = new Bank()
            {
                BranchId = branchId,
                Name=request.Name,
                InsertTime=DateTime.Now,
                InsertByUserId=userId
            };
            await _bankRepository.CreateAsync(bank);
            return new ResultDto
            {
                IsSuccess = true,
                Message=Messages.RegisterSuccess
            };
        }
    }
}
