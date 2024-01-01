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
    public interface IAddOurServiceService
    {
        Task<ResultDto> Execute(RequestOurServiceDto request,Guid userId,Guid branchId);
    }
    public class AddOurServiceService: IAddOurServiceService
    {
        private readonly IMongoRepository<OurService> _ourServiceRepository;

        public AddOurServiceService(IMongoRepository<OurService> ourServiceRepository)
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
                    if (request.PriceWithProfit < 0)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.PRICE_INVALID
                        };
                    }
                    OurService ourService = new OurService()
                    {
                        BranchId = branchId,
                        Title = request.Title,
                        InsertByUserId = userId,
                        Description = request.Description,
                        PriceWithProfit = request.PriceWithProfit,
                        RawPrice = request.RawPrice,
                    };
                    await _ourServiceRepository.CreateAsync(ourService,session);
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
    }
}
