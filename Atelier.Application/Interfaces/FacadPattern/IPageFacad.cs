using Atelier.Application.Services.Pages.Commands;
using Atelier.Application.Services.Pages.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IPageFacad
    {
        IAddPageService AddPageService { get; }
        IEditPageService EditPageService { get; }
        IRemovePageService RemovePageService { get; }
        IGetAllPageService GetAllPageService { get; }
    }
}
