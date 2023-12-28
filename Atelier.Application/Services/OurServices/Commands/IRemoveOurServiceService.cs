using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Persons.Commands;
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
    public interface IRemoveOurServiceService
    {
        Task<ResultDto> Execute(Guid id, Guid userId);
    }
    public class RemoveOurServiceService : IRemoveOurServiceService
    {
        private readonly IMongoRepository<OurService> _ourServiceRepository;

        public RemoveOurServiceService(IMongoRepository<OurService> ourServiceRepository)
        {
            _ourServiceRepository = ourServiceRepository;
        }
        public async Task<ResultDto> Execute(Guid id, Guid userId)
        {
            var person = await _ourServiceRepository.GetAsync(id);
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

            await _ourServiceRepository.UpdateAsync(person);
            return new ResultDto()
            {
                IsSuccess=true,
                Message=Messages.Remove
            };
        }
    }
}
