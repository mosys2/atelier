using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Common.Helpers;
using Atelier.Domain.MongoEntities;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Persons.Commands
{
    public interface IAddPersonService
    {
        Task<ResultDto> Execute(RequestPersonDto request, Guid userId, Guid branchId);
    }

    public class AddPersonService : IAddPersonService
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Job> _jobRepository;
        private readonly IMongoRepository<PersonType> _personTypeRepository;

        public AddPersonService(
            IMongoRepository<Person> personRepository,
            IMongoRepository<Job> jobRepository,
            IMongoRepository<PersonType> personTypeRepository)
        {
            _personRepository = personRepository;
            _jobRepository = jobRepository;
            _personTypeRepository = personTypeRepository;
        }

        public async Task<ResultDto> Execute(RequestPersonDto request, Guid userId, Guid branchId)
        {
            using (var session = await _personRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();

                    // Check for duplicate mobile number
                    if (!string.IsNullOrEmpty(request.Mobile.Trim()))
                    {
                        var duplicateMobile = await CheckDuplicateAsync(branchId, p => p.Mobile == request.Mobile.Trim(), Messages.DuplicateMobile, session);
                        if (!duplicateMobile.IsSuccess)
                            return duplicateMobile;
                    }

                    // Check for duplicate national code
                    if (!string.IsNullOrEmpty(request.NationalCode.Trim()))
                    {
                        var duplicateNationalCode = await CheckDuplicateAsync(branchId, p => p.NationalCode == request.NationalCode.Trim(), Messages.DuplicateNationalCode, session);
                        if (!duplicateNationalCode.IsSuccess)
                            return duplicateNationalCode;
                    }

                    var job = await GetEntityByIdAsync(request.JobId, branchId, _jobRepository, session);
                    var personType = await GetEntityByIdAsync(request.PersonTypeId, branchId, _personTypeRepository, session);

                    var person = new Person
                    {
                        Name = request.Name.Trim(),
                        Family = request.Family.Trim(),
                        BranchId = branchId,
                        InsertByUserId = userId,
                        Address = request.Address?.Trim(),
                        Description = request.Description?.Trim(),
                        FullName = $"{request.Name.Trim()} {request.Family.Trim()}",
                        InsertTime = DateTime.Now,
                        Job = job,
                        PersonType = personType,
                        Mobile = request.Mobile.Trim(),
                        NationalCode = request.NationalCode.Trim(),
                        Phone = request.Phone?.Trim(),
                    };

                    await _personRepository.CreateAsync(person, session);
                    await session.CommitTransactionAsync();

                    return new ResultDto { IsSuccess = true, Message = Messages.RegisterSuccess };
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }

        private async Task<ResultDto> CheckDuplicateAsync(Guid branchId, Func<Person, bool> condition, string errorMessage, IClientSessionHandle session)
        {
            var duplicateEntity = await _personRepository.GetAsync(p => p.BranchId == branchId && condition(p), session);

            return duplicateEntity != null
                ? new ResultDto { IsSuccess = false, Message = errorMessage }
                : new ResultDto { IsSuccess = true };
        }

        private Task<T> GetEntityByIdAsync<T>(Guid? entityId, Guid branchId, IMongoRepository<T> repository, IClientSessionHandle session) where T : IEntity
        {
            return entityId.HasValue
                ? repository.GetAsync(e => e.BranchId == branchId && e.Id == entityId, session)
                : Task.FromResult<T>(default);
        }

    }
}
