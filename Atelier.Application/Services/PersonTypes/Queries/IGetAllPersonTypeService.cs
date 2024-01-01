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
        Task<ResultDto<List<ResponsePersonTypeDto>>> Execute(Guid branchId, RequstPaginateDto pagination);
    }
    public class GetAllPersonType : IGetAllPersonTypeService
    {
        private readonly IMongoRepository<PersonType> _personTypeRepository;
        public GetAllPersonType(IMongoRepository<PersonType> personTypeRepository)
        {
            _personTypeRepository=personTypeRepository;
        }
        public async Task<ResultDto<List<ResponsePersonTypeDto>>> Execute(Guid branchId, RequstPaginateDto pagination)
        {
            using (var session = await _personTypeRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var (personTypes, total) = await _personTypeRepository.GetAllAsync(q => q.BranchId == branchId, pagination,session);
                    var personTypeList = personTypes.Select(s => new ResponsePersonTypeDto
                    {
                        Id = s.Id,
                        Title = s.Title,
                    }).ToList();
                    await session.CommitTransactionAsync();
                    return new ResultDto<List<ResponsePersonTypeDto>>
                    {
                        Data = personTypeList,
                        Total = total,
                        IsSuccess = true,
                        Message = Messages.GetSuccess
                    };
                }
                catch (Exception ex)
                {

                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto<List<ResponsePersonTypeDto>> { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
           
        }
    }
}
