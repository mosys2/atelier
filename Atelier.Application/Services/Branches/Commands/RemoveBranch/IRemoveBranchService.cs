using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.Commands.RemoveBranch
{
    public interface IRemoveBranchService
    {
        Task<ResultDto> Execute(string branchId, string removeByUserId);
    }
    public class RemoveBranchService : IRemoveBranchService
    {
        private readonly IDatabaseContext _context;
        public RemoveBranchService(IDatabaseContext context)
        {
            _context = context; 
        }
        public async Task<ResultDto> Execute(string branchId, string removeByUserId)
        {
            var branch=await _context.Branches.FindAsync(branchId);
            if (branch == null)
            {
                return new ResultDto()
                {
                    IsSuccess=false,
                    Message=Messages.NotFind
                };
            }
            branch.IsRemoved = true;
            branch.RemoveByUserId = removeByUserId;
            branch.RemoveTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return new ResultDto()
            {
                IsSuccess=true,
                Message=Messages.Delete
            };
        }
    }
}
