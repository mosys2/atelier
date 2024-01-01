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
            using (var session = await _personTypeRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    PersonType personType = new PersonType()
                    {
                        BranchId = branchId,
                        InsertByUserId = userId,
                        Title = request.Title,
                        InsertTime = DateTime.Now,
                    };
                    await _personTypeRepository.CreateAsync(personType, session);
                    await session.CommitTransactionAsync();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Messages.RegisterSuccess
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
