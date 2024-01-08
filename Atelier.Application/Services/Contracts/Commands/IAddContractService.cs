using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Atelier.Domain.Entities.Users;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Contracts.Commands
{
    public interface IAddContractService
    {
        Task<ResultDto> Execute(RequestContractDto request, Guid userId, Guid branchId);
    }
    public class AddContractService : IAddContractService
    {
        private readonly IMongoRepository<Contract> _contractRepository;
        private readonly IMongoRepository<Person> _personRepository;

        public AddContractService(IMongoRepository<Contract> contractRepository, IMongoRepository<Person> personRepository)
        {
            _contractRepository = contractRepository;
            _personRepository = personRepository;
        }

        public async Task<ResultDto> Execute(RequestContractDto request, Guid userId, Guid branchId)
        {
            using (var session = await _contractRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();

                    var person = await _personRepository.GetAsync(p => p.BranchId==branchId && p.Id==request.PersonId, session); ;
                    if (person == null) { return new ResultDto { IsSuccess = false, Message= Messages.PersonNotFound }; }
                    //last transaction for last contract number
                    var LastContract = await _contractRepository.GetLastAsync(c => c.BranchId==branchId, session);

                    List<PaymentTerms> paymentTerms = new List<PaymentTerms>();
                    if (request.PaymentTermsList!=null)
                    {
                        foreach (var paymentTerm in request.PaymentTermsList)
                        {
                            bool checkTypePay = TypePay.TypePayList().Contains(paymentTerm.TypePay);
                            if (!checkTypePay)
                            {
                                return new ResultDto
                                {
                                    IsSuccess = false,
                                    Message = Messages.NOT_FOUND_TypePay
                                };

                            }

                            paymentTerms.Add(new PaymentTerms()
                            {
                                Id=Guid.NewGuid(),
                                Amount = paymentTerm.Amount,
                                Date = paymentTerm.Date,
                                Description = paymentTerm.Description,
                                InsertByUserId=userId,
                                InsertTime=DateTime.Now,
                                Title=paymentTerm.Title,
                                TypePay=paymentTerm.TypePay,
                            });
                        }
                    }

                    List<ServiceContract> serviceContracts = new List<ServiceContract>();
                    double totalFactor = 0;
                    if (request.ServiceContractList!=null)
                    {
                        foreach (var serviceContract in request.ServiceContractList)
                        {
                            bool checkUnit = ContractServiceUnit.ContractServiceUnitList().Contains(serviceContract.Unit);
                            if (!checkUnit)
                            {
                                return new ResultDto
                                {
                                    IsSuccess = false,
                                    Message = Messages.NOT_FOUND_UNIT
                                };
                            }
                            //محاسبه هزینه
                            double totalPrice = 0;
                            totalPrice=await TotalService(serviceContract.Unit, serviceContract.PriceWithProfit, serviceContract.Value, serviceContract.Discont);
                            serviceContracts.Add(new ServiceContract()
                            {
                                Id=Guid.NewGuid(),
                                Title=serviceContract.Title,
                                InsertByUserId = userId,
                                Discont=serviceContract.Discont,
                                PriceWithProfit=serviceContract.PriceWithProfit,
                                RawPrice=serviceContract.RawPrice,
                                Unit=serviceContract.Unit,
                                Total=totalPrice,
                                InsertTime=DateTime.Now,
                                Value=serviceContract.Value,
                            });
                            totalFactor+=totalPrice;
                        }
                    }

                    Contract contract = new Contract()
                    {
                        BranchId = branchId,
                        Person=person,
                        CeremonyAddress=request.CeremonyAddress,
                        CeremonyEndDateTime=request.CeremonyEndDateTime,
                        CeremonyStartDateTime=request.CeremonyStartDateTime,
                        ContractTitle=request.ContractTitle,
                        ContractDate=request.ContractDate,
                        ContractNumber=LastContract==null ? 100 : LastContract.ContractNumber+1,
                        Description=request.Description,
                        Discount=request.Discount,
                        ExecutorContract=request.ExecutorContract,
                        GuildNumber=request.GuildNumber,
                        UnitAddress=request.UnitAddress,
                        UnitHead=request.UnitHead,
                        UnitMobile=request.UnitMobile,
                        UnitPhone=request.UnitPhone,
                        PaymentTermsList=paymentTerms,
                        ServiceContractList=serviceContracts,
                        Total=totalFactor-request.Discount,
                        InsertTime = DateTime.Now,
                        InsertByUserId = userId
                    };

                    await _contractRepository.CreateAsync(contract, session);
                    await session.CommitTransactionAsync();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Messages.RegisterSuccess
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
        public async Task<double> TotalService(int unit, double priceWithProfit, double value, double discount)
        {
            double total = 0;
            if (unit == ContractServiceUnit.Time)
            {
                total = ((value/60)*priceWithProfit)-discount;
            }
            else if (unit==ContractServiceUnit.Number)
            {
                total= (value*priceWithProfit)-discount;
            }
            return total;
        }
    }
}
