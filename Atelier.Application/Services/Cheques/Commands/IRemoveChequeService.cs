using Atelier.Application.Interfaces.Repository;
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
    public interface IRemoveChequeService
    {
        Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId);
    }
    public class RemoveChequeService : IRemoveChequeService
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        public RemoveChequeService(IMongoRepository<Cheque> chequeRepository)
        {
            _chequeRepository = chequeRepository;
        }
        public async Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId)
        {
            using (var session = await _chequeRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var cheque = await _chequeRepository.GetAsync(id, session);
                    if (cheque == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = Messages.PersonNotFound,
                        };
                    }
                    cheque.IsRemoved = true;
                    cheque.RemoveTime = DateTime.Now;
                    cheque.RemoveByUserId = userId;

                    await _chequeRepository.UpdateAsync(cheque, session);
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
