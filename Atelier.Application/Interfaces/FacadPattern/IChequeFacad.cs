using Atelier.Application.Services.Cheques.Commands;
using Atelier.Application.Services.Cheques.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IChequeFacad
    {
        IAddChequeService AddChequeService { get; }
        IEditChequeService EditChequeService { get; }
        IRemoveChequeService RemoveChequeService { get; }
        IGetAllChequeService GetAllChequesService { get; }
    }

}
