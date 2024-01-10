using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Contracts.Queries
{
    public interface IGetAllContractService
    {
        Task<ResultDto<List<ResponseContractDto>>> Execute(Guid branchId, RequstPaginateDto pagination);
    }
    public class GetAllContractService : IGetAllContractService
    {
        private readonly IMongoRepository<Contract> _contractRepository;
        public GetAllContractService(IMongoRepository<Contract> contractRepository)
        {
            _contractRepository=contractRepository;
        }

        public async Task<ResultDto<List<ResponseContractDto>>> Execute(Guid branchId, RequstPaginateDto pagination)
        {
            using (var session = await _contractRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var (contracts, total) = await _contractRepository.GetAllAsync(q => q.BranchId == branchId, pagination, session);
                    var contractList = contracts.Select(r => new ResponseContractDto
                    {
                        Id = r.Id,
                        ContractDate = r.ContractDate,
                        ContractNumber =r.ContractNumber,
                        PersonFullName = r.Person.FullName ?? "",
                        Title = r.ContractTitle,
                        TotalPrice = r.Total
                    }).ToList();
                    await session.CommitTransactionAsync();
                    return new ResultDto<List<ResponseContractDto>>
                    {
                        Data = contractList,
                        Total = total,
                        IsSuccess = true,
                        Message = Messages.GetSuccess
                    };
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto<List<ResponseContractDto>> { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }
    }
}
