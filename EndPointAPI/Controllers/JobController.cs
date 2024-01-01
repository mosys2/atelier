using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Jobs.Commands;
using Atelier.Application.Services.Jobs.Queries;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobFacad _jobFacad;
        private readonly Guid userId;
        private readonly Guid branchId;
        public JobController(IJobFacad jobFacad, ClaimsPrincipal user)
        {
            _jobFacad=jobFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
            branchId = Guid.Parse(ClaimUtility.GetBranchId(user) ?? "");
        }
        // GET: api/<JobController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]

        public async Task<IActionResult> Get([FromQuery] int page, int pageSize = 20)
        {

            if (branchId==Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFoundUserOrBranch
                });
            }
            var result = await _jobFacad.GetAllJobService.Execute(branchId, new RequstPaginateDto { Page = page, PageSize = pageSize });
            return Ok(result);
        }
        // POST api/<JobController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]

        public async Task<IActionResult> Post([FromBody] RequestJobDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }
            if (userId == Guid.Empty || branchId==Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFoundUserOrBranch
                });
            }
           var result=await _jobFacad.AddJobService.Execute(request, userId, branchId);
            return Ok(result);
        }
    }
}
