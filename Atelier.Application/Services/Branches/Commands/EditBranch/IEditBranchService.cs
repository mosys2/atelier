using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.Commands.EditBranch
{
    public interface IEditBranchService
    {
        Task<ResultDto> Execute(string id,string CurrentUserId,EditBranchDto editBranch);
    }
    public class EditBranchService : IEditBranchService
    {
        private readonly IDatabaseContext _context;
        public EditBranchService(IDatabaseContext context)
        {
            _context = context;
        }
        public  async Task<ResultDto> Execute(string id, string CurrentUserId, EditBranchDto editBranch)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
             return new ResultDto
             { 
                 IsSuccess = false,
                 Message=Messages.NotFind
             };
            }
            var findAtelier = await _context.AtelierBases.FindAsync(editBranch.AtelierBaseId);
            if(findAtelier == null)
            {
                return new ResultDto
                {
                    IsSuccess= false,
                    Message=Messages.NotfindAtelierbase
                };
            }
            branch.Address = editBranch.Address;
            branch.PhoneNumber = editBranch.PhoneNumber;
            branch.Status = editBranch.Status;
            branch.Code = editBranch.Code;
            branch.AtelierBaseId = findAtelier.Id;
            branch.Description = editBranch.Description;
            branch.ExpireDate = editBranch.ExpireDate;
            branch.Title = editBranch.Title;
            branch.StatusDescription = editBranch.StatusDescription;
            branch.UpdateTime = DateTime.Now;
            branch.UpdateByUserId = CurrentUserId;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess=true,
                Message=Messages.MessageUpdate
            };
        }
    }
}
