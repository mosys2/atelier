using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Ateliers.Commands;
using Atelier.Application.Services.Pages.Commands;
using Atelier.Application.Services.Pages.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Pages.FacadPattern
{
    public class PageFacad : IPageFacad
    {
        private readonly IDatabaseContext _context;
        public PageFacad(IDatabaseContext context)
        {
            _context = context;
        }
        private IAddPageService _addPageService;
        private IEditPageService _editPageService;
        private IRemovePageService _removePageService;
        private IGetAllPageService _getAllPageService;
        public IAddPageService AddPageService
        {
            get { return _addPageService = _addPageService ?? new AddPageService(_context); }
        }

        public IEditPageService EditPageService
        {
            get { return _editPageService = _editPageService ?? new EditPageService(_context); }
        }

        public IRemovePageService RemovePageService
        {
            get { return _removePageService = _removePageService ?? new RemovePageService(_context); }

        }

        public IGetAllPageService GetAllPageService
        {
            get { return _getAllPageService = _getAllPageService ?? new GetAllPageService(_context); }

        }
    }
}
