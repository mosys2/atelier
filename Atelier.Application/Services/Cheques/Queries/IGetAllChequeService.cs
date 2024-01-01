using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Cheques.Queries
{
    public interface IGetAllChequeService
    {
        Task<ResultDto<List<ResponseChequeDto>>> Execute(Guid branchId, RequstPaginateDto pagination);
    }
    public class GetAllChequeService : IGetAllChequeService
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        public GetAllChequeService(IMongoRepository<Cheque> chequeRepository)
        {
            _chequeRepository = chequeRepository;
        }
        public async Task<ResultDto<List<ResponseChequeDto>>> Execute(Guid branchId, RequstPaginateDto pagination)
        {
            using (var session = await _chequeRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var (cheques, total) = await _chequeRepository.GetAllAsync(q => q.BranchId == branchId, pagination,session);
                    var chequeList = cheques.Select(r => new ResponseChequeDto
                    {
                        Id = r.Id,
                        AccountNumber = r.AccountNumber,
                        BankName = r.Bank.Name,
                        BankBranch = r.BankBranch,
                        ChequeNumber = r.ChequeNumber,
                        Date = r.Date,
                        Description = r.Description,
                        FinancialType = FinancialType.GetTitle(r.FinancialType),
                        PersonName = r.Person.Name + " " + r.Person.Family,
                        Phone = r.Phone,
                        Price = r.Price,
                        SpentInTheName = r.SpentInTheName,
                        StatusCheque = StatusCheque.GetTitle(r.StatusCheque),
                        StatusRegistered = StatusRegistered.GetTitle(r.StatusRegistered)
                    }).ToList();
                    await session.CommitTransactionAsync();
                    return new ResultDto<List<ResponseChequeDto>>
                    {
                        Data = chequeList,
                        Total = total,
                        IsSuccess = true,
                        Message = Messages.GetSuccess
                    };
                }
                catch (Exception ex)
                {

                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto<List<ResponseChequeDto>> { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }
    }
}
