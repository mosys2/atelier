using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.OurServices.Commands
{
    public interface IEditOurServiceService
    {
        Task<ResultDto> Execute(RequestOurServiceDto request, Guid userId, Guid branchId);
    }
    public class EditOurService : IEditOurServiceService
    {
        private readonly IMongoRepository<OurService> _ourServiceRepository;
        public EditOurService(IMongoRepository<OurService> ourServiceRepository)
        {
            _ourServiceRepository = ourServiceRepository;
        }
        public async Task<ResultDto> Execute(RequestOurServiceDto request, Guid userId, Guid branchId)
        {
            using (var session = await _ourServiceRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    if (request.Id == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.INVALID_ID
                        };
                    }
                    var ourService = await _ourServiceRepository.GetAsync(request.Id.Value,session);
                    if (ourService == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.NOT_FOUND_RECORD
                        };
                    }
                    ourService.Title = request.Title;
                    ourService.Description = request.Description;
                    ourService.PriceWithProfit = request.PriceWithProfit;
                    ourService.RawPrice = request.RawPrice;
                    ourService.UpdateByUserId = userId;
                    ourService.UpdateTime = DateTime.Now;

                    await _ourServiceRepository.UpdateAsync(ourService,session);
                    await session.CommitTransactionAsync();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Messages.MessageUpdate
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
