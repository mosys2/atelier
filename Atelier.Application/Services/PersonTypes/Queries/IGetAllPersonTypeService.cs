using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.PersonTypes.Queries
{
    public interface IGetAllPersonTypeService
    {
        Task<ResultDto<List<ResponsePersonTypeDto>>> Execute(Guid branchId);
    }
    public class GetAllPersonType : IGetAllPersonTypeService
    {
        private readonly IMongoRepository<PersonType> _personTypeRepository;
        public GetAllPersonType(IMongoRepository<PersonType> personTypeRepository)
        {
            _personTypeRepository=personTypeRepository;
        }
        public async Task<ResultDto<List<ResponsePersonTypeDto>>> Execute(Guid branchId)
        {
            var (personTypes, total) = await _personTypeRepository.GetAllAsync(q => q.BranchId==branchId, null);
            var personTypeList = personTypes.Select(s => new ResponsePersonTypeDto
            {
                Id = s.Id,
                Title  = s.Title,
            }).ToList();

            return new ResultDto<List<ResponsePersonTypeDto>>
            {
                Data=personTypeList,
                Total=total,
                IsSuccess = true,
                Message=Messages.GetSuccess
            };
        }
    }
}
