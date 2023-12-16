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
        [HttpGet]
        public async Task<IActionResult> Get(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFind
                });
            }
            var result = await _getAllBranches.Excute(new RequestBranchDto { AtelierBaseId=Id});
            return Ok(result);
        }

        // GET api/<BranchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BranchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BranchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BranchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
