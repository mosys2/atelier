using Atelier.Application.Services.OurServices.Commands;
using Atelier.Application.Services.OurServices.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IOurServiceFacad
    {
         IAddOurServiceService AddOurServiceService { get; }
        IGetOurServiceService GetOurServiceService { get; }
        IEditOurServiceService EditOurService { get; }
        IRemoveOurServiceService RemoveOurServiceService { get; }
    }
}
