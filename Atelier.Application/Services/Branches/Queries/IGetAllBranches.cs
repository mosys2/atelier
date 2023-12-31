﻿using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.AtelierApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Branches.Queries
{
    public interface IGetAllBranches
    {
        Task<ResultDto<List<ResultBranchDto>>> Excute(RequestBranchDto request);
    }
    public class GetAllBranches : IGetAllBranches
    {
        private readonly IDatabaseContext _context;
        public GetAllBranches(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<ResultBranchDto>>> Excute(RequestBranchDto request)
        {
            var atelierBase=await _context.AtelierBases.FindAsync(request.AtelierBaseId);
            if (atelierBase == null)
            {
                return new ResultDto<List<ResultBranchDto>>
                {
                    Data=null,
                    IsSuccess = false,
                    Message="آتلیه مورد نظر یافت نشد!"
                };
            }
            DateTime dateNow = DateTime.Now;
            var result=await _context.Branches.Where(b => b.AtelierBaseId == atelierBase.Id).Select(p=>new ResultBranchDto
            {
                Id = p.Id,
                Title = p.Title,
                Status=p.Status,
                Address=p.Address,
                Code=p.Code,
                Description=p.Description,
                isExpier=p.ExpireDate <dateNow,
                ExpireDate=p.ExpireDate,
                PhoneNumber=p.PhoneNumber,
                StatusDescription = p.StatusDescription,
                AtelierBaseId=p.AtelierBaseId,
                
            })
            .ToListAsync();

            return new ResultDto<List<ResultBranchDto>> {
                Data=result,
                IsSuccess=true,
            };
        }
    }
}
