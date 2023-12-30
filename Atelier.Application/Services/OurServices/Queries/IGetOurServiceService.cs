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
            var (ourServices,total) =await _ourServiceRepository.GetAllAsync(q => q.BranchId==branchId, null);
            var ourServiceList= ourServices.Select(o =>new ResponseOurServiceDto
            {
                Id=o.Id,
                PriceWithProfit=o.PriceWithProfit,
                RawPrice=o.RawPrice,
                Title=o.Title,
            }).ToList();
            return new ResultDto<List<ResponseOurServiceDto>>()
            {
                Data=ourServiceList,
                Total=total,
                IsSuccess=true,
                Message=Messages.GetSuccess
            };
        }
    }


}
