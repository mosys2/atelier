using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.PersonTypes.Commands
{
    public interface IAddPersonTypeService
    {
        Task<ResultDto> Execute(RequestPersonTypeDto request, Guid userId, Guid branchId);
    }
    public class AddPersonTypeService: IAddPersonTypeService
    {
        private readonly IMongoRepository<PersonType> _personTypeRepository;
        public AddPersonTypeService(IMongoRepository<PersonType> personTypeRepository)
        {
            _personTypeRepository=personTypeRepository;
        }
        public async Task<ResultDto> Execute(RequestPersonTypeDto request, Guid userId, Guid branchId)
        {
            PersonType personType = new PersonType()
            {
                BranchId = branchId,
                InsertByUserId = userId,
                Title = request.Title,
                InsertTime = DateTime.Now,
            };
            await _personTypeRepository.CreateAsync(personType);
            return new ResultDto
            {
                IsSuccess = true,
                Message=Messages.RegisterSuccess
            };
        }
    }
}
