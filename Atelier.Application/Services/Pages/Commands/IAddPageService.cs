using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Pages.Commands
{
    public interface IAddPageService
    {
        Task<ResultDto> Execute(RequestPageDto request, Guid userId);
    }
    public class AddPageService : IAddPageService
    {
        private readonly IDatabaseContext _context;
        public AddPageService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(RequestPageDto request, Guid userId)
        {
            Page pages = new Page()
            {
                 Id=Guid.NewGuid().ToString(),
                 Name=request.Name,
                 InsertByUserId=userId.ToString(),
                 InsertTime=DateTime.Now,
            };
            await _context.Pages.AddAsync(pages);
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.INSERT_PAGE
            };
        }
    }
}
