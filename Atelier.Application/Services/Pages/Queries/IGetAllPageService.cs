using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Pages.Queries
{
    public interface IGetAllPageService
    {
        Task<ResultDto<List<ResultListPageDto>>> Excute();
    }
    public class GetAllPageService : IGetAllPageService
    {
        private readonly IDatabaseContext _context;
        public GetAllPageService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<ResultListPageDto>>> Excute()
        {
            var result = await _context.Pages
            .OrderByDescending(p => p.InsertTime)
            .Select(p => new ResultListPageDto
            {
                Id = p.Id,
                Name = p.Name,
                InsertTime=p.InsertTime,
            }).ToListAsync();

            return new ResultDto<List<ResultListPageDto>>
            {
                Data = result,
                IsSuccess = true,
            };
        }
    }
}
