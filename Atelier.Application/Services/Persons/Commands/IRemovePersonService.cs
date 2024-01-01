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
            using (var session = await _personRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    var person = await _personRepository.GetAsync(id,session);
                    if (person == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = Messages.PersonNotFound,
                        };
                    }
                    person.IsRemoved = true;
                    person.RemoveTime = DateTime.Now;
                    person.RemoveByUserId = userId;

                    await _personRepository.UpdateAsync(person, session);
                    await session.CommitTransactionAsync();
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = Messages.Remove
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
