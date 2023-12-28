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
        Task<ResultDto<List<ResponseOurServiceDto>>> Execute();
    }
    public class GetOurServiceService : IGetOurServiceService
    {
        private readonly IMongoRepository<OurService> _ourServiceRepository;
        public GetOurServiceService(IMongoRepository<OurService> ourServiceRepository)
        {
            _ourServiceRepository=ourServiceRepository;
        }
        public async Task<ResultDto<List<ResponseOurServiceDto>>> Execute()
        {
            var ourServices= _ourServiceRepository.GetAllAsync().Result.Select(o =>new ResponseOurServiceDto
            {
                Id=o.Id,
                PriceWithProfit=o.PriceWithProfit,
                RawPrice=o.RawPrice,
                Title=o.Title,
            }).ToList();
            return new ResultDto<List<ResponseOurServiceDto>>()
            {
                Data=ourServices,
                IsSuccess=true,
                Message=Messages.GetSuccess
            };
        }
    }


}
