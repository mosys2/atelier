using Atelier.Application.Services.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IContractFacad
    {
        IAddContractService AddContractService { get; }
    }
}
