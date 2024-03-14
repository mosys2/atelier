using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Pages.Commands
{
    public interface IRemovePageService
    {
        Task<ResultDto> Execute(string pageId, Guid removeByUserId);
    }
    public class RemovePageService : IRemovePageService
    {
        private readonly IDatabaseContext _context;
        public RemovePageService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string pageId, Guid removeByUserId)
        {
            var page = await _context.Pages.FindAsync(pageId);
            if (page == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                };
            }
            page.IsRemoved = true;
            page.RemoveByUserId = removeByUserId.ToString();
            page.RemoveTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.Delete
            };
        }
    }
}
