using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Ateliers.Commands.RemoveAtelier
{
    public interface IRemoveAtelierService
    {
        Task<ResultDto> Execute(string atelierId,string removeByUserId);
    }
    public class RemoveAtelierService : IRemoveAtelierService
    {
        private readonly IDatabaseContext _context;
        public RemoveAtelierService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string atelierId, string removeByUserId)
        {
            var atelier = await _context.AtelierBases.FindAsync(atelierId);
            if(atelier == null)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=Messages.NotFind
                };
            }
            var listBranch=await _context.Branches.Where(r=>r.AtelierBaseId==atelier.Id).ToListAsync();
            //remove Logical Branch
            foreach (var branch in listBranch)
            {
                branch.IsRemoved = true;
                branch.RemoveByUserId=removeByUserId;
                branch.RemoveTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            atelier.IsRemoved = true;
            atelier.RemoveByUserId= removeByUserId;
            atelier.RemoveTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess=true,
                Message=Messages.Delete
            };
        }
    }
}
