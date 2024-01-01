using Atelier.Application.Services.PersonTypes.Commands;
using Atelier.Application.Services.PersonTypes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IPersonTypeFacad
    {
        IAddPersonTypeService AddPersonTypeService { get; }
        IGetAllPersonTypeService GetAllPersonTypeService { get; }
    }
}
