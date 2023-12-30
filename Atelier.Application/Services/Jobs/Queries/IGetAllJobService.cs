﻿using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Jobs.Commands;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Jobs.Queries
{
    public interface IGetAllJobService
    {
        Task<ResultDto<List<ResponseJobListDto>>> Execute(Guid branchId);
    }
    public class GetAllJobService : IGetAllJobService
    {
        private readonly IMongoRepository<Job> _jobRepository;
        public GetAllJobService(IMongoRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<ResultDto<List<ResponseJobListDto>>> Execute(Guid branchId)
        {
            var (jobs,total) = await _jobRepository.GetAllAsync(q => q.BranchId==branchId, null);
                var jobList= jobs.Select(s => new ResponseJobListDto
                {
                    Id = s.Id,
                    Title  = s.Title,
                }).ToList();

            return new ResultDto<List<ResponseJobListDto>>
            {
                Data=jobList,
                Total=total,
                IsSuccess = true,
                Message=Messages.GetSuccess
            };
        }


    }
}
