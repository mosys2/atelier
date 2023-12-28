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
            if(request.PriceWithProfit<0)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.PRICE_INVALID
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
            await _ourServiceRepository.CreateAsync(ourService);
            return new ResultDto
            {
                IsSuccess = true,
                Message=Messages.RegisterSuccess
            };
        }
    }
}
