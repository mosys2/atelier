using Atelier.Application.Interfaces.Contexts;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Ateliers.Queries;
using Atelier.Application.Services.Branches.Commands.AddBranch;
using Atelier.Application.Services.Branches.Commands.ChangeStatusBranch;
using Atelier.Application.Services.Branches.Commands.EditBranch;
using Atelier.Application.Services.Branches.Commands.RemoveBranch;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Application.Services.Branches.Queries.GetDetailBranch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.FacadPattern
{
    public class BranchFacad : IBranchFacad
    {
        private readonly IDatabaseContext _context;
        public BranchFacad(IDatabaseContext context)
        {
            _context = context;
        }
        //
        private  IGetAllBranches _getAllBranches;
        private  IAddBranchService _addBranchService;
        private  IRemoveBranchService _removeBranchService;
        private  IGetDetailBranchService _getDetailBranchService;
        private  IEditBranchService _editBranchService;
        private  IChangeStatusBranchService _changeStatusBranchService;
        public IGetAllBranches GetAllBranches
        {
            get
            {
                return _getAllBranches = _getAllBranches ?? new GetAllBranches(_context);
            }
        }

        public IAddBranchService AddBranchService
        {
            get
            {
                return _addBranchService = _addBranchService ?? new AddBranchService(_context);
            }
        }

        public IRemoveBranchService RemoveBranchService
        {
            get
            {
                return _removeBranchService = _removeBranchService ?? new RemoveBranchService(_context);
            }
        }

        public IGetDetailBranchService GetDetailBranchService
        {
            get
            {
                return _getDetailBranchService = _getDetailBranchService ?? new GetDetailBranchService(_context);
            }
        }

        public IEditBranchService EditBranchService
        {
            get
            {
                return _editBranchService = _editBranchService ?? new EditBranchService(_context);
            }
        }

        public IChangeStatusBranchService ChangeStatusBranchService
        {
            get
            {
                return _changeStatusBranchService = _changeStatusBranchService ?? new ChangeStatusBranchService(_context);
            }
        }
    }
}
