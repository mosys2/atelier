using Atelier.Application.Services.Branches.Queries;
using Microsoft.AspNetCore.Mvc;
using Atelier.Common.Dto;
using Atelier.Common.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IGetAllBranches _getAllBranches;
        public BranchController(IGetAllBranches getAllBranches) { 
            _getAllBranches = getAllBranches;
        }
        // GET: api/<BranchController>
        [HttpGet("{atelierBaseId}")]
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
