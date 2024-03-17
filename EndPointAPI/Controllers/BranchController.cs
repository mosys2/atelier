using Atelier.Application.Services.Branches.Queries;
using Microsoft.AspNetCore.Mvc;
using Atelier.Common.Dto;
using Atelier.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Atelier.Application.Services.Branches.Commands.AddBranch;
using Atelier.Common.Helpers;
using Atelier.Application.Services.Branches.Commands.RemoveBranch;
using Atelier.Application.Services.Branches.Queries.GetDetailBranch;
using Atelier.Application.Services.Branches.Commands.EditBranch;
using Atelier.Application.Services.Branches.Commands.ChangeStatusBranch;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.infrastructure.HasAccessPage;
using Atelier.Domain.Entities.Users;
using System.Security.Claims;
using EndPointAPI.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchFacad _branchFacad;
        private readonly IHasAccessPageService _hasAccessPageService;
        private readonly Guid userId;
        public BranchController(IBranchFacad branchFacad, ClaimsPrincipal user, IHasAccessPageService hasAccessPageService) 
        {
            _branchFacad = branchFacad;
             userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            _hasAccessPageService = hasAccessPageService;
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
            var result = await _branchFacad.AddBranchService.Execute
                (
                new AddBranchDto
                {
                    AtelierBaseId=branch.AtelierBaseId,
                    Address = branch.Address,
                    Code = branch.Code,
                    Description = branch.Description,
                    ExpireDate =branch.ExpireDate,
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
            // بررسی مجوز ورود
            var hasAccess = await _hasAccessPageService.Execute(userId,"شعبه");
            if (!hasAccess)
            {
                return StatusCode(403, new ResultDto
                {
                    IsSuccess = false,
                    Message = "Access Denied"
                }); 
            }
            var result = await _branchFacad.GetAllBranches.Excute(new RequestBranchDto { AtelierBaseId=atelierBaseId });
            return Ok(result);
        }
        [HttpPost("ChangeStatus/{branchId}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> ChangeStatus(string branchId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _branchFacad.ChangeStatusBranchService.Execute(branchId);
            return Ok(result);
        }
        // GET api/<UsersController>/5
        [HttpGet("Detail/{branchId}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> GetDetail(string branchId)
        {
            if (string.IsNullOrEmpty(branchId))
            {
                return NotFound(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                });
            }
            var result = await _branchFacad.GetDetailBranchService.Execute(branchId);
            return Ok(result);
        }
        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditBranchDto editBranch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _branchFacad.EditBranchService.Execute(id, editByUserId, editBranch);
            return Ok(result);
        }
        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var remByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _branchFacad.RemoveBranchService.Execute(id, remByUserId);
            return Ok(result);
        }
    }
}
