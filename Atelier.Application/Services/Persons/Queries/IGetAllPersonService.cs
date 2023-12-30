using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Persons.Queries
{
    public interface IGetAllPersonService
    {
        Task<ResultDto<List<ResponsePersonDto>>> Execute(Guid branchId, RequstPaginateDto pagination);
    }
    public class GetAllPersonService: IGetAllPersonService
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Job> _jobRepository;
        private readonly IMongoRepository<PersonType> _personTypeRepository;

        public GetAllPersonService(IMongoRepository<Person> personRepository,
            IMongoRepository<Job> jobRepository ,
            IMongoRepository<PersonType> personTypeRepository)
        {
            _personRepository = personRepository;
            _jobRepository = jobRepository;
            _personTypeRepository = personTypeRepository;
        }
        public async Task<ResultDto<List<ResponsePersonDto>>> Execute(Guid branchId, RequstPaginateDto pagination)
        {
            var (persons, total) = await _personRepository.GetAllAsync(q => q.BranchId == branchId, pagination);
            var resultPersons=persons.Select(r => new ResponsePersonDto
             {
                 Id =r.Id,
                 Name = r.Name,
                 Family=r.Family,
                 JobTitle =r.Job?.Title,
                 Mobile = r.Mobile,
                 NationalCode = r.NationalCode,
                 PersonTypeTitle =r.PersonType?.Title,
                 Phone = r.Phone,
             }).ToList();

            return new ResultDto<List<ResponsePersonDto>>
            {
                Data=resultPersons,
                Total=total,
                IsSuccess = true,
                Message=Messages.GetSuccess
            };
        }
    }
}
