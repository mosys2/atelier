﻿using Atelier.Application.Services.Branches.Queries;
using Microsoft.AspNetCore.Mvc;
using Atelier.Common.Dto;
using Atelier.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Atelier.Application.Services.Branches.Commands.AddBranch;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IGetAllBranches _getAllBranches;
        private readonly IAddBranchService _addBranchService;
        public BranchController(IGetAllBranches getAllBranches,IAddBranchService addBranchService) { 
            _getAllBranches = getAllBranches;
            _addBranchService = addBranchService;
        }
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post(RequestAddBranchDto branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _addBranchService.Execute
                (
                new AddBranchDto
                {
                    AtelierBaseId=branch.AtelierBaseId,
                    Address = branch.Address,
                    Code = branch.Code,
                    Description = branch.Description,
                    ExpireDate = branch.ExpireDate,
                    InsertByUserId=userId,
                    PhoneNumber = branch.PhoneNumber,
                    Status = branch.Status,
                    StatusDescription = branch.StatusDescription,
                    Title = branch.Title,
                }
                );
            return Ok(result);
        }
        // GET: api/<BranchController>
        [HttpGet("{atelierBaseId}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get(string atelierBaseId)
        {
            if (string.IsNullOrEmpty(atelierBaseId))
            {
                return NotFound(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFind
                });
            }
            var result = await _getAllBranches.Excute(new RequestBranchDto { AtelierBaseId=atelierBaseId });
            return Ok(result);
        }

       
    }
}
