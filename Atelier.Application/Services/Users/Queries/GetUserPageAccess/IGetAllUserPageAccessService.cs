using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Queries.GetUserPageAccess
{
    public interface IGetAllUserPageAccessService
    {
        Task <ResultDto<ResultUserPageAccessDto>> Execute();
    }
    public class GetAllUserPageAccessService : IGetAllUserPageAccessService
    {
        private readonly IDatabaseContext _context;
        public GetAllUserPageAccessService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<ResultUserPageAccessDto>> Execute()
        {
            var userPageAccessList =_context.PageAccess
            .Include(pa => pa.Page)
            .Include(pa => pa.User)
            .AsQueryable();

            return new ResultDto<ResultUserPageAccessDto>
            {
                Data = new ResultUserPageAccessDto
                {
                    DetailAccessPages = userPageAccessList.Select(p => new DetailAccessPageDto
                    {
                        NamePage = p.Page.Name,
                        FullName=p.User.FullName,
                        Url = p.Page.Url
                    }).ToList()
                },
                IsSuccess=true
            };
          
        }
    }
}
