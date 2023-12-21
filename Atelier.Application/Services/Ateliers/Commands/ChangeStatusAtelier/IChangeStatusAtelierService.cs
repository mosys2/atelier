using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Ateliers.Commands.ChangeStatusAtelier
{
    public interface IChangeStatusAtelierService
    {
        Task<ResultDto> Execute(string atelierId);
    }
    public class ChangeStatusAtelierService : IChangeStatusAtelierService
    {
        private readonly IDatabaseContext _context;
        public ChangeStatusAtelierService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string atelierId)
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
            atelier.Status = !atelier.Status;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.ChangeStatusAtelier
            };
        }
    }
}
