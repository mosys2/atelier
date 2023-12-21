using Atelier.Application.Services.Branches.Commands.AddBranch;
using Atelier.Application.Services.Branches.Commands.ChangeStatusBranch;
using Atelier.Application.Services.Branches.Commands.EditBranch;
using Atelier.Application.Services.Branches.Commands.RemoveBranch;
using Atelier.Application.Services.Branches.Queries.GetDetailBranch;
using Atelier.Application.Services.Branches.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.FacadPattern
{
    public interface IBranchFacad
    {
        IGetAllBranches GetAllBranches{ get; }
        IAddBranchService AddBranchService { get; }
        IRemoveBranchService RemoveBranchService { get; }
        IGetDetailBranchService GetDetailBranchService { get; }
        IEditBranchService EditBranchService { get; }
        IChangeStatusBranchService ChangeStatusBranchService { get; }
    }
}
