using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.Jobs.Queries;
using Atelier.Domain.MongoEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Jobs.FacadPattern
{
    public class JobFacad : IJobFacad
    {
        private readonly IMongoRepository<Job> _jobRepository;
        public JobFacad(IMongoRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        private IAddJobService _addJobService;
        private IGetAllJobService _getAllJobService;
        public IAddJobService AddJobService
        {
            get
            {
                return _addJobService = _addJobService ?? new AddJobService(_jobRepository);
            }
        }

        public IGetAllJobService GetAllJobService
        {
            get
            {
                return _getAllJobService = _getAllJobService ?? new GetAllJobService(_jobRepository);
            }
        }
    }
}
