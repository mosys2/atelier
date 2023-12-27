using Atelier.Application.Services.Persons.Commands;
using Atelier.Application.Services.Persons.Queries;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IAddPersonService _addPerson;
        private readonly IGetAllPersonService _getAllPerson;
        private readonly IEditPersonService _editPerson;
        private readonly IRemovePersonService _removePerson;
        public PersonController(IAddPersonService addPerson,
            IGetAllPersonService getAllPerson,
            IEditPersonService editPerson,
            IRemovePersonService removePerson)
        {
            _addPerson = addPerson;
            _getAllPerson = getAllPerson;
            _editPerson=editPerson;
            _removePerson=removePerson;
        }

        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            Guid branchId = Guid.Parse(User.Claims.ToList()[1].Value ?? "");

            if (branchId==Guid.Empty)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.NotFoundUserOrBranch
                });
            }
            var result = await _getAllPerson.Execute(branchId);
            return Ok(result);

        }

        // POST api/<PersonController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestPersonDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }

            Guid userId = Guid.Parse(User.Claims.ToList()[0].Value ?? "");
            Guid branchId = Guid.Parse(User.Claims.ToList()[1].Value ?? "");

            var result = await _addPerson.Execute(request, userId, branchId);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put([FromBody] RequestPersonDto request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultDto
                {
                    IsSuccess = false,
                    Message=Messages.InvalidForm
                });
            }

            Guid userId = Guid.Parse(User.Claims.ToList()[0].Value ?? "");
            Guid branchId = Guid.Parse(User.Claims.ToList()[1].Value ?? "");

            var result = await _editPerson.Execute(request, userId, branchId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = Guid.Parse(User.Claims.ToList()[0].Value ?? "");
            Guid branchId = Guid.Parse(User.Claims.ToList()[1].Value ?? "");

            var result = await _removePerson.Execute(id,userId, branchId);
            return Ok(result);
        }

    }
}
