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
        Task<ResultDto<List<ResponseChequeDto>>> Execute(Guid branchId);
    }
    public class GetAllChequeService : IGetAllChequeService
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        public GetAllChequeService(IMongoRepository<Cheque> chequeRepository)
        {
            _chequeRepository = chequeRepository;
        }
        public async Task<ResultDto<List<ResponseChequeDto>>> Execute(Guid branchId)
        {
            var result = _chequeRepository.GetAllAsync(q => q.BranchId == branchId)
                .Result.Select(r => new ResponseChequeDto
                {
                    Id = r.Id,
                    AccountNumber = r.AccountNumber,
                   BankName=r.Bank.Name,
                   ChequeNumber=r.ChequeNumber,
                   Date=r.Date,
                   Description = r.Description,
                   FinancialType = r.FinancialType,
                   PersonName=r.Person.FullName,
                   Phone = r.Phone,
                   Price = r.Price,
                   SpentInTheName=r.SpentInTheName,
                   StatusCheque=r.StatusCheque,
                   StatusRegistered = r.StatusRegistered

                }).ToList();

            return new ResultDto<List<ResponseChequeDto>>
            {
                Data = result,
                IsSuccess = true,
                Message = Messages.GetSuccess
            };
        }
    }
}
