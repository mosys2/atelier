using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Persons.Commands
{
    public interface IRemovePersonService
    {
        Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId);
    }
    public class RemovePersonService : IRemovePersonService
    {
        private readonly IMongoRepository<Person> _personRepository;

        public RemovePersonService(IMongoRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<ResultDto> Execute(Guid id, Guid userId, Guid branchId)
        {
            var person = await _personRepository.GetAsync(id);
            if (person == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message=Messages.PersonNotFound,
                };
            }
            person.IsRemoved = true;
            person.RemoveTime = DateTime.Now;
            person.RemoveByUserId = userId;

            await _personRepository.UpdateAsync(person);
            return new ResultDto()
            {
                IsSuccess=true,
                Message=Messages.Remove
            };
        }
    }
}
