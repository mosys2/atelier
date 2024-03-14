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
    public interface IEditPageService
    {
        Task<ResultDto> Execute(string id, Guid CurrentUserId, EditPageDto editPage);
    }
    public class EditPageService : IEditPageService
    {
        private readonly IDatabaseContext _context;
        public EditPageService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string id, Guid CurrentUserId, EditPageDto editPage)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                };
            }
            page.Name = editPage.Name;
            page.UpdateByUserId = CurrentUserId.ToString();
            page.UpdateTime=DateTime.Now;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = Messages.MessageUpdate
            };
        }
    }
}
