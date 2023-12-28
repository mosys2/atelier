﻿using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Persons.Commands
{
    public interface IEditPersonService
    {
        Task<ResultDto> Execute(RequestPersonDto request, Guid userId, Guid branchId);
    }
    public class EditPersonService : IEditPersonService
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Job> _jobRepository;
        private readonly IMongoRepository<PersonType> _persontypeRepository;

        public EditPersonService(IMongoRepository<Person> personRepository,
            IMongoRepository<Job> jobRepository,
            IMongoRepository<PersonType> persontypeRepository)
        {
            _personRepository = personRepository;
            _jobRepository = jobRepository;
            _persontypeRepository = persontypeRepository;
        }
        public async Task<ResultDto> Execute(RequestPersonDto request, Guid userId, Guid branchId)
        {
            if (request.Id == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                };
            }
            var curentPerson =await _personRepository.GetAsync(request.Id.Value);
            if(curentPerson==null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.PersonNotFound
                };
            }
            //چک کردن شماره تلفن و یا کد ملی تکراری
            //خالی وارد کردن هر دو باید ثبت شود

            if (!request.Mobile.Trim().IsNullOrEmpty() && request.Mobile!=curentPerson.Mobile)
            {
                var findUser = await _personRepository.GetAllAsync(p =>
                p.BranchId==branchId &&
                p.Mobile==request.Mobile.Trim());

                if (findUser.Count>0)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message=Messages.DuplicateMobile
                    };
                }
            }
            if (!request.NationalCode.Trim().IsNullOrEmpty() && request.NationalCode!=curentPerson.NationalCode)
            {
                var findUser = await _personRepository.GetAllAsync(p =>
                p.BranchId==branchId &&
                p.NationalCode==request.NationalCode.Trim());

                if (findUser.Count>0)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message=Messages.DuplicateNationalCode
                    };
                }
            }

            //گرفتن فیلد ها ی شغل و نوع شخص در صورت انتخاب
            Job job = null;
            PersonType personType = null;
            if (request.JobId.HasValue)
            {
                job =_jobRepository.GetAllAsync(j => j.Id==request.JobId).Result.First();
            }
            if (request.PersonTypeId.HasValue)
            {
                personType =_persontypeRepository.GetAllAsync(p => p.Id==request.PersonTypeId).Result.First();
            }

           
            curentPerson.Name = request.Name.Trim();
            curentPerson.Family = request.Family.Trim();
            curentPerson.BranchId = branchId;
            curentPerson.InsertByUserId = userId;
            curentPerson.Address = request.Address?.Trim();
            curentPerson.Description = request.Description?.Trim();
            curentPerson.FullName = request.FullName?.Trim();
            curentPerson.UpdateByUserId = userId;
            curentPerson.UpdateTime = DateTime.Now;
            curentPerson.Job=job;
            curentPerson.PersonType=personType;
            curentPerson.Mobile=request.Mobile.Trim();
            curentPerson.NationalCode=request.NationalCode.Trim();
            curentPerson.Phone =request.Phone?.Trim();
           
            await _personRepository.UpdateAsync(curentPerson);
            return new ResultDto
            {
                IsSuccess = true,
                Message=Messages.MessageUpdate
            };
        }
    }
}
