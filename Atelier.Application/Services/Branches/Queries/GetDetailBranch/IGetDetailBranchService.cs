using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.Queries.GetDetailBranch
{
    public interface IGetDetailBranchService
    {
        Task<ResultDto<GetDetailBranchDto>> Execute(string branchId);
    }
    public class GetDetailBranchService : IGetDetailBranchService
    {
        private readonly IDatabaseContext _context;
        public GetDetailBranchService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<GetDetailBranchDto>> Execute(string branchId)
        {
            var branch = await _context.Branches.Include(a=>a.AtelierBase).Where(b=>b.Id==branchId).FirstOrDefaultAsync();
            if(branch == null)
            {
                return new ResultDto<GetDetailBranchDto>
                {
                    IsSuccess = false,
                    Data = new GetDetailBranchDto { },
                    Message = Messages.NotFind,
                };
            }
            return new ResultDto<GetDetailBranchDto>
            {
                IsSuccess = true,
                Data = new GetDetailBranchDto
                {
                    BranchId=branch.Id,
                    Address=branch.Address,
                    AtelierBaseId=branch.AtelierBase.Id,
                    AtelierBaseTitle=branch.AtelierBase.Name,
                    Code=branch.Code,
                    Description = branch.Description,
                    ExpireDate = branch.ExpireDate,
                    PhoneNumber=branch.PhoneNumber,
                    Status = branch.Status,
                    StatusDescription = branch.StatusDescription,
                    Title = branch.Title
                },
            };
        }
    }
}
