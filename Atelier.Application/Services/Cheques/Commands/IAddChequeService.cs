﻿using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Cheques.Commands
{
    public interface IAddChequeService
    {
        Task<ResultDto> Execute(RequestChequeDto requestCheque,Guid userId,Guid branchId);
    }
    public class AddChequeService : IAddChequeService
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        private readonly IMongoRepository<Bank> _bankRepository;
        private readonly IMongoRepository<Person> _personRepository;

        public AddChequeService(IMongoRepository<Cheque> chequeRepository, IMongoRepository<Bank> bankRepository, IMongoRepository<Person> personRepository)
        {
            _chequeRepository = chequeRepository;
            _bankRepository = bankRepository;
            _personRepository = personRepository;

        }
        public async Task<ResultDto> Execute(RequestChequeDto requestCheque, Guid userId, Guid branchId)
        {
            using (var session = await _chequeRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    bool checkFinancialType = FinancialType.FinancialTypeList().Contains(requestCheque.FinancialType);
                    if (!checkFinancialType)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.FinancialTypeNotFound
                        };
                    }
                    var bank = await _bankRepository.GetAsync(b => b.BranchId == branchId && b.Id == requestCheque.BankId,session);
                    if (bank == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.BankNotFound
                        };
                    }
                    var check = await _chequeRepository.GetAsync(b => b.BranchId == branchId && b.ChequeNumber == requestCheque.ChequeNumber,session);
                    if (check != null)
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
                            Message = Messages.StatusChequeNotFound
                        };
                    }
                    bool checkStatusRegistered = StatusRegistered.StatusRegisteredList().Contains(requestCheque.StatusRegistered);
                    if (!checkStatusRegistered)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.StatusRegisterChequeNotFound
                        };
                    }
                    var person = await _personRepository.GetAsync(b => b.BranchId == branchId && b.Id == requestCheque.PersonId, session);
                    if (person == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.MessageNotfindUser
                        };
                    }
                    //
                    Cheque cheque = new Cheque()
                    {
                        BranchId = branchId,
                        InsertByUserId = userId,
                        StatusRegistered = requestCheque.StatusRegistered,
                        FinancialType = requestCheque.FinancialType,
                        InsertTime = DateTime.Now,
                        Date = requestCheque.Date,
                        ChequeNumber = requestCheque.ChequeNumber.Trim(),
                        Bank = bank,
                        BankBranch = requestCheque.BankBranch,
                        Person = person,
                        AccountNumber = requestCheque.AccountNumber,
                        Price = requestCheque.Price,
                        Phone = requestCheque.Phone,
                        StatusCheque = requestCheque.StatusCheque,
                        SpentInTheName = requestCheque.SpentInTheName,
                        Description = requestCheque.Description,
                    };
                    await _chequeRepository.CreateAsync(cheque,session);
                    await session.CommitTransactionAsync();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Messages.InsertCheque
                    };
                }
                catch (Exception ex)
                {

                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }
    }
}
