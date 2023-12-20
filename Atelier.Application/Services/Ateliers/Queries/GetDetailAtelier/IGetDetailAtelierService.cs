using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Ateliers.Queries.GetDetailAtelier
{
    public interface IGetDetailAtelierService
    {
        Task<ResultDto<GetDetailAtelierDto>> Execute(string atelierId);
    }
    public class GetDetailAtelierService : IGetDetailAtelierService
    {
        private readonly IDatabaseContext _context;
        public GetDetailAtelierService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<GetDetailAtelierDto>> Execute(string atelierId)
        {
            var atelier = await _context.AtelierBases.FindAsync(atelierId);
            if(atelier == null)
            {
                return new ResultDto<GetDetailAtelierDto>
                {
                    IsSuccess = false,
                    Message = Messages.NotFind,
                    Data = new GetDetailAtelierDto { }
                };
            }
            return new ResultDto<GetDetailAtelierDto>
            {
                IsSuccess=true,
                Data=new GetDetailAtelierDto
                {
                AtelierId=atelier.Id,
                Name=atelier.Name,
                Description=atelier.Description,
                Status = atelier.Status,
                InsertTime=atelier.InsertTime,
                StatusMessage = atelier.StatusMessage
                }
            };
        }
    }
}
