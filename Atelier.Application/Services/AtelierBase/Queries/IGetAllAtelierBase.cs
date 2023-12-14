using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.AtelierBase.Queries
{
    public interface IGetAllAtelierBase
    {
        Task<ResultDto<List<ResultListAtelierBaseDto>>> Excute();
    }
    public class GetAllAtelierBase : IGetAllAtelierBase
    {
        private readonly IDatabaseContext _context;
        public GetAllAtelierBase(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<ResultListAtelierBaseDto>>> Excute()
        {
            var result=await _context.AtelierBases.Include(b=>b.Branches).Select(p=>new ResultListAtelierBaseDto{
                Id = p.Id,
                Name = p.Name,
                InsertTime=p.InsertTime,
                Status=p.Status,
                branchCount=p.Branches.Count()
            }).ToListAsync();

            return new ResultDto<List<ResultListAtelierBaseDto>> {
                Data=result,
                IsSuccess=true,
            };
        }
    }
}
