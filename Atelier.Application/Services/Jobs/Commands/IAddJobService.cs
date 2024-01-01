using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Jobs.Commands
{
    public interface IAddJobService
    {
        Task<ResultDto> Execute(RequestJobDto request,Guid userId,Guid branchId);
    }
    public class AddJobService: IAddJobService
    {
        private readonly IMongoRepository<Job> _jobRepository;
        public AddJobService(IMongoRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<ResultDto> Execute(RequestJobDto request, Guid userId, Guid branchId)
        {
            using (var session = await _jobRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    Job job = new Job()
                    {
                        BranchId = branchId,
                        InsertByUserId = userId,
                        Title = request.Title,
                        InsertTime = DateTime.Now,
                    };
                    await _jobRepository.CreateAsync(job, session);
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
