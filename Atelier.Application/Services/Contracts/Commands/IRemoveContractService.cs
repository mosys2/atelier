using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Contracts.Commands
{
    public interface IRemoveContractService
    {
        Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId);
    }
    public class RemoveContractService : IRemoveContractService
    {
        private readonly IMongoRepository<Contract> _contractRepository;
        private readonly IMongoRepository<Person> _personRepository;

        public RemoveContractService(IMongoRepository<Contract> contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public async Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId)
        {

            using (var session = await _contractRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var contract = await _contractRepository.GetAsync(c => c.BranchId==branchId && c.Id==id, session);
                    if (contract == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = Messages.NOT_FOUND_CONTRACT,
                        };
                    }
                    contract.IsRemoved = true;
                    contract.RemoveTime = DateTime.Now;
                    contract.RemoveByUserId = userId;

                    await _contractRepository.UpdateAsync(contract, session);
                    await session.CommitTransactionAsync();
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = Messages.Remove
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
