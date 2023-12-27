using Atelier.Application.Services.Persons.Commands;
using Atelier.Application.Services.Persons.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IPersonFacad
    {
         IAddPersonService AddPersonService { get; }
         IGetAllPersonService GetAllPersonService { get; }
         IEditPersonService EditPersonService { get; }
         IRemovePersonService RemovePersonService { get; }
    }
}
