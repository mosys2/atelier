using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Ateliers.Commands.EditAtelier
{
    public interface IEditAtelierService
    {
        Task<ResultDto> Execute(string id,string? CurrentUserId, EditAtelierDto editAtelier);
    }
    public class EditAtelierService : IEditAtelierService
    {
        private readonly IDatabaseContext _context;
        public EditAtelierService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id,string? CurrentUserId, EditAtelierDto editAtelier)
        {
            var atelier = await _context.AtelierBases.FindAsync(id);
            if(atelier==null)
            {
                return new ResultDto
                {
                    IsSuccess= false,
                    Message=Messages.NotFind
                };
            }
            atelier.StatusMessage = editAtelier.StatusMessage;
            atelier.Description = editAtelier.Description;
            atelier.Status = editAtelier.Status;
            atelier.Name = editAtelier.Name;
            atelier.UpdateByUserId = CurrentUserId;
            atelier.UpdateTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess=true,
                Message=Messages.MessageUpdate
            };
        }
    }
}
