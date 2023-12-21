using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.Commands.ChangeStatusBranch
{
    public interface IChangeStatusBranchService
    {
        Task<ResultDto> Execute(string branchId);
    }
    public class ChangeStatusBranchService : IChangeStatusBranchService
    {
        private readonly IDatabaseContext _context;
        public ChangeStatusBranchService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string branchId)
        {
            var branch = await _context.AtelierBases.FindAsync(branchId);
            if (branch == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                };
            }
            branch.Status = !branch.Status;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.ChangeStatusAtelier
            };
        }
    }
}
