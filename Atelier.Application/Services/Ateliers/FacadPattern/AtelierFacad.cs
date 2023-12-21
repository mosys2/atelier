using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Ateliers.Commands;
using Atelier.Application.Services.Ateliers.Commands.ChangeStatusAtelier;
using Atelier.Application.Services.Ateliers.Commands.EditAtelier;
using Atelier.Application.Services.Ateliers.Commands.RemoveAtelier;
using Atelier.Application.Services.Ateliers.Queries;
using Atelier.Application.Services.Ateliers.Queries.GetDetailAtelier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Ateliers.FacadPattern
{
    public class AtelierFacad : IAtelierFacad
    {
        private readonly IDatabaseContext _context;
        public AtelierFacad(IDatabaseContext context)
        {
            _context = context;
        }
        private  IGetAllAtelierBase _getAllAtelierBase;
        private  IAddAtelierService _addAtelierService;
        private  IRemoveAtelierService _removeAtelierService;
        private  IGetDetailAtelierService _getDetailAtelierService;
        private  IEditAtelierService _editAtelierService;
        private  IChangeStatusAtelierService _changeStatusAtelierService;
        public IGetAllAtelierBase GetAllAtelierBase
        {
            get
            {
                return _getAllAtelierBase = _getAllAtelierBase ?? new GetAllAtelierBase(_context);
            }
        }

        public IAddAtelierService AddAtelierService
        {
            get
            {
                return _addAtelierService = _addAtelierService ?? new AddAtelierService(_context);
            }
        }

        public IRemoveAtelierService RemoveAtelierService
        {
            get
            {
                return _removeAtelierService = _removeAtelierService ?? new RemoveAtelierService(_context);
            }
        }

        public IGetDetailAtelierService GetDetailAtelierService
        {
            get
            {
                return _getDetailAtelierService = _getDetailAtelierService ?? new GetDetailAtelierService(_context);
            }
        }

        public IEditAtelierService EditAtelierService
        {
            get
            {
                return _editAtelierService = _editAtelierService ?? new EditAtelierService(_context);
            }
        }

        public IChangeStatusAtelierService ChangeStatusAtelierService
        {
            get
            {
                return _changeStatusAtelierService = _changeStatusAtelierService ?? new ChangeStatusAtelierService(_context);
            }
        }
    }
}
