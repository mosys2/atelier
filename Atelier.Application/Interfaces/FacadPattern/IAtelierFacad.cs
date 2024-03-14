using Atelier.Application.Services.Ateliers.Commands.ChangeStatusAtelier;
using Atelier.Application.Services.Ateliers.Commands.EditAtelier;
using Atelier.Application.Services.Ateliers.Commands.RemoveAtelier;
using Atelier.Application.Services.Ateliers.Commands;
using Atelier.Application.Services.Ateliers.Queries.GetDetailAtelier;
using Atelier.Application.Services.Ateliers.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IAtelierFacad
    {
        IGetAllAtelierBase GetAllAtelierBase{  get; }
        IAddAtelierService AddAtelierService { get; }
        IRemoveAtelierService RemoveAtelierService { get; }
        IGetDetailAtelierService GetDetailAtelierService { get; }
        IEditAtelierService EditAtelierService { get; }
        IChangeStatusAtelierService ChangeStatusAtelierService { get; }
    }
}
