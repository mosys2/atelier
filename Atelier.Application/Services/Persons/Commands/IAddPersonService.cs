using Atelier.Application.Interfaces.Repository;
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
            //چک کردن شماره تلفن و یا کد ملی تکراری
            //خالی وارد کردن هر دو باید ثبت شود

            if (!request.Mobile.Trim().IsNullOrEmpty())
            {
                var findUser = await _personRepository.GetAllAsync(p => p.BranchId==branchId &&
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

            if (!request.NationalCode.Trim().IsNullOrEmpty())
            {
                var findUser = await _personRepository.GetAllAsync(p => p.BranchId==branchId &&
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
                job = _jobRepository.GetAllAsync(j => j.Id==request.JobId).Result.First();
            }
            if (request.PersonTypeId.HasValue)
            {
                personType =_persontypeRepository.GetAllAsync(p => p.Id==request.PersonTypeId).Result.First();
            }
            Person person = new Person()
            {
                Name = request.Name.Trim(),
                Family=request.Family.Trim(),
                BranchId=branchId,
                InsertByUserId=userId,
                Address=request.Address?.Trim(),
                Description=request.Description?.Trim(),
                FullName= request.Name.Trim()+" "+ request.Family.Trim(),
                InsertTime=DateTime.Now,
                Job=job,
                PersonType=personType,
                Mobile=request.Mobile.Trim(),
                NationalCode=request.NationalCode.Trim(),
                Phone=request.Phone?.Trim(),
            };
            await _personRepository.CreateAsync(person);
            return new ResultDto
            {
                IsSuccess = true,
                Message=Messages.RegisterSuccess
            };
        }
    }
}
