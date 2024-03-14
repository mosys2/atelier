using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.ChangePageAccess
{
    public interface IChangePageAccessService
    {
        Task<ResultDto> Execute(string roleId, List<string> pageId);
    }
    public class ChangePageAccessService : IChangePageAccessService
    {
        public async Task<ResultDto> Execute(string roleId, List<string> pageId)
        {
            
        }
    }
}
