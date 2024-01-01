using Atelier.Application.Services.Banks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IBankFacad
    {
        IAddNewBankService AddNewBankService { get; }
        IEditBankService EditBankService { get; }
    }
}
