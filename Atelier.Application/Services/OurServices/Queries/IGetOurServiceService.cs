using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.OurServices.Queries
{
    public interface IGetOurServiceService
    {
        Task<ResultDto<List<ResponseOurServiceDto>>> Execute(Guid branchId);
    }
    public class GetOurServiceService : IGetOurServiceService
    {
        private readonly IMongoRepository<OurService> _ourServiceRepository;
        public GetOurServiceService(IMongoRepository<OurService> ourServiceRepository)
        {
            _ourServiceRepository=ourServiceRepository;
        }
        public async Task<ResultDto<List<ResponseOurServiceDto>>> Execute(Guid branchId)
        {
            using (var session = await _ourServiceRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var (ourServices, total) = await _ourServiceRepository.GetAllAsync(q => q.BranchId == branchId, null,session);
                    var ourServiceList = ourServices.Select(o => new ResponseOurServiceDto
                    {
                        Id = o.Id,
                        PriceWithProfit = o.PriceWithProfit,
                        RawPrice = o.RawPrice,
                        Title = o.Title,
                    }).ToList();
                    await session.CommitTransactionAsync();
                    return new ResultDto<List<ResponseOurServiceDto>>()
                    {
                        Data = ourServiceList,
                        Total = total,
                        IsSuccess = true,
                        Message = Messages.GetSuccess
                    };
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto<List<ResponseOurServiceDto>>() { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
            
        }
    }


}
