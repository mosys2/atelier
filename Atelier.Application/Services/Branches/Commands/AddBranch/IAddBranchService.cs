using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.Commands.AddBranch
{
    public interface IAddBranchService
    {
        Task<ResultDto> Execute(AddBranchDto branch);
    }
    public class AddBranchService : IAddBranchService
    {
        private readonly IDatabaseContext _context;
        public AddBranchService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(AddBranchDto branch)
        {
            var checkAtelir =await _context.AtelierBases.FindAsync(branch.AtelierBaseId);
            if(checkAtelir == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotfindAtelierbase
                };
            }
            var checkCode =await _context.Branches.Where(c => c.Code == branch.Code).FirstOrDefaultAsync();
            if(checkCode!=null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.ExistBranchCode
                };
            }
            Branch addBranch = new Branch()
            {
                Id=Guid.NewGuid().ToString(),
                Title=branch.Title,
                Address=branch.Address,
                AtelierBaseId=checkAtelir.Id,
                Code=branch.Code,
                Description = branch.Description,
                PhoneNumber=branch.PhoneNumber,
                ExpireDate=branch.ExpireDate,
                InsertTime=DateTime.Now,
                Status=branch.Status,
                StatusDescription = branch.StatusDescription,
                InsertByUserId=branch.InsertByUserId,
            };
            await _context.Branches.AddAsync(addBranch);
            await _context.SaveChangesAsync();
            return new ResultDto()
            {
                IsSuccess=true,
                Message=Messages.InsertBranch
            };
        }
    }
}
