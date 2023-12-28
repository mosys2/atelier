﻿using Amazon.Runtime.Internal;
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
            var curentCheque = _chequeRepository.GetAllAsync(p => p.Id == requestCheque.Id).Result.FirstOrDefault();
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

            var person = _personRepository.GetAllAsync(b => b.Id == requestCheque.PersonId).Result.FirstOrDefault();
            if (person == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.MessageNotfindUser
                };
            }
            Cheque cheque = new Cheque()
            {
                Id=curentCheque.Id,
                BranchId = branchId,
                UpdateByUserId = userId,
                StatusRegistered = requestCheque.StatusRegistered,
                FinancialType = requestCheque.FinancialType,
                UpdateTime = DateTime.Now,
                Date = requestCheque.Date,
                ChequeNumber = requestCheque.ChequeNumber.Trim(),
                Bank = bank,
                Person = person,
                AccountNumber = requestCheque.AccountNumber,
                Price = requestCheque.Price,
                Phone = requestCheque.Phone,
                StatusCheque = requestCheque.StatusCheque,
                SpentInTheName = requestCheque.SpentInTheName,
                Description = requestCheque.Description,
            };
            await _chequeRepository.UpdateAsync(cheque);
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.MessageUpdate
            };
        }
    }
}