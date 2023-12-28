using Amazon.Runtime.Internal;
using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Cheques.Commands
{
    public interface IEditChequeService
    {
        Task<ResultDto> Execute(RequestChequeDto requestCheque, Guid userId, Guid branchId);
    }
    public class EditChequeService : IEditChequeService
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        private readonly IMongoRepository<Bank> _bankRepository;
        private readonly IMongoRepository<Person> _personRepository;

        public EditChequeService(IMongoRepository<Cheque> chequeRepository, IMongoRepository<Bank> bankRepository, IMongoRepository<Person> personRepository)
        {
            _chequeRepository = chequeRepository;
            _bankRepository = bankRepository;
            _personRepository = personRepository;

        }
        public async Task<ResultDto> Execute(RequestChequeDto requestCheque, Guid userId, Guid branchId)
        {
            if(requestCheque.Id==null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFind
                };
            }
            var curentCheque =await _chequeRepository.GetAsync(requestCheque.Id.Value);
            if (curentCheque == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFoundCheque
                };
            }
            if (!requestCheque.ChequeNumber.Trim().IsNullOrEmpty() && requestCheque.ChequeNumber != curentCheque.ChequeNumber)
            {
                var findCheque = await _chequeRepository.GetAllAsync(p =>
                p.BranchId == branchId &&
                p.ChequeNumber == requestCheque.ChequeNumber.Trim());

                if (findCheque.Count > 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = Messages.DuplicateChequeNumber
                    };
                }
            }

            //check Value
            bool checkFinancialType = FinancialType.FinancialTypeList().Contains(requestCheque.FinancialType);
            if (!checkFinancialType)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.FinancialTypeNotFound
                };
            }

            var bank = _bankRepository.GetAllAsync(b => b.Id == requestCheque.BankId).Result.FirstOrDefault();
            if (bank == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.BankNotFound
                };
            }

            bool checkChequeNumber = _chequeRepository.GetAllAsync(b => b.ChequeNumber == requestCheque.ChequeNumber).Result.Count > 0;
            if (checkChequeNumber)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.ChequeNumberExists
                };
            }

            bool checkStatusCheque = StatusCheque.StatusChequeList().Contains(requestCheque.StatusCheque);
            if (!checkStatusCheque)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.FinancialTypeNotFound
                };
            }

            bool checkStatusRegistered = StatusRegistered.StatusRegisteredList().Contains(requestCheque.StatusRegistered);
            if (!checkStatusRegistered)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.FinancialTypeNotFound
                };
            }

            var person = _personRepository.GetAllAsync(b => b.BranchId == branchId && b.Id == requestCheque.PersonId).Result.FirstOrDefault();
            if (person == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.MessageNotfindUser
                };
            }
            curentCheque.BranchId = branchId;
            curentCheque.UpdateByUserId = userId;
            curentCheque.StatusRegistered = requestCheque.StatusRegistered;
            curentCheque.FinancialType = requestCheque.FinancialType;
            curentCheque.UpdateTime = DateTime.Now;
            curentCheque.Date = requestCheque.Date;
            curentCheque.ChequeNumber = requestCheque.ChequeNumber.Trim();
            curentCheque.Bank = bank;
            curentCheque.BankBranch = requestCheque.BankBranch;
            curentCheque.Person = person;
            curentCheque.AccountNumber = requestCheque.AccountNumber;
            curentCheque.Price = requestCheque.Price;
            curentCheque.Phone = requestCheque.Phone;
            curentCheque.StatusCheque = requestCheque.StatusCheque;
            curentCheque.SpentInTheName = requestCheque.SpentInTheName;
            curentCheque.Description = requestCheque.Description;
            await _chequeRepository.UpdateAsync(curentCheque);
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.MessageUpdate
            };
        }
    }
}
