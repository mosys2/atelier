using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.Jobs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IJobFacad
    {
        IAddJobService AddJobService { get; }
        IGetAllJobService GetAllJobService { get; }
    }
}
