﻿using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Common.Helpers;
using Atelier.Domain.MongoEntities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IMongoRepository<PersonType> _persontypeRepository;

        public AddPersonService(IMongoRepository<Person> personRepository,
            IMongoRepository<Job> jobRepository,
            IMongoRepository<PersonType> persontypeRepository)
        {
            _personRepository = personRepository;
            _jobRepository = jobRepository;
            _persontypeRepository = persontypeRepository;
        }
        public async Task<ResultDto> Execute(RequestPersonDto request, Guid userId, Guid branchId)
        {
            using (var session = await _personRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();

                    if (!request.Mobile.IsNullOrEmpty())
                    {
                        var findUser = await _personRepository.GetAsync(p => p.BranchId == branchId &&
                        p.Mobile == request.Mobile, session);

                        if (findUser != null)
                        {
                            return new ResultDto
                            {
                                IsSuccess = false,
                                Message = Messages.DuplicateMobile
                            };
                        }
                    }
                    if (!request.NationalCode.IsNullOrEmpty())
                    {
                        var findUser = await _personRepository.GetAsync(p => p.BranchId == branchId &&
                        p.NationalCode == request.NationalCode, session);

                        if (findUser != null)
                        {
                            return new ResultDto
                            {
                                IsSuccess = false,
                                Message = Messages.DuplicateNationalCode
                            };
                        }
                    }


                    Job? job = request.JobId.HasValue
                             ? await _jobRepository.GetAsync(j => j.BranchId == branchId && j.Id == request.JobId, session)
                             : null;

                    PersonType? personType = request.PersonTypeId.HasValue
                        ? await _persontypeRepository.GetAsync(p => p.BranchId == branchId && p.Id == request.PersonTypeId, session)
                        : null;

                    Person person = new Person()
                    {
                        Name = request.Name.Trim(),
                        Family = request.Family.Trim(),
                        BranchId = branchId,
                        InsertByUserId = userId,
                        Address = request.Address?.Trim(),
                        Description = request.Description?.Trim(),
                        FullName = request.Name.Trim() + " " + request.Family.Trim(),
                        InsertTime = DateTime.Now,
                        Job = job,
                        PersonType = personType,
                        Mobile = request.Mobile,
                        NationalCode = request.NationalCode,
                        Phone = request.Phone?.Trim(),
                    };
                    await _personRepository.CreateAsync(person, session);
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