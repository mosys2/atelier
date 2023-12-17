using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Atelier.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atelier.Common.Constants;

namespace Atelier.Application.Services.Ateliers.Commands
{
    public interface IAddAtelierService
    {
        Task<ResultDto> Execute(AddAtelierDto addAtelier);
    }
    public class AddAtelierService : IAddAtelierService
    {
        private readonly IDatabaseContext _context;
        public AddAtelierService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(AddAtelierDto addAtelier)
        {
            AtelierBase atelier = new AtelierBase()
            {
                Id=Guid.NewGuid().ToString(),
                Name = addAtelier.Name,
                Description = addAtelier.Description,
                Status = addAtelier.Status,
                StatusMessage = addAtelier.StatusMessage,
                InsertTime= DateTime.Now,
                InsertByUserId=addAtelier.CurrentUserId,
            }; 
           await _context.AtelierBases.AddAsync(atelier);
           await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.InsertAtelier
            };
        }
    }
}
