using Atelier.Application.Services.Users.Commands.AddUser;
using Atelier.Application.Services.Users.Commands.DeleteUser;
using Atelier.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IAddEmployeeService _employeeService;
        private readonly IRemoveEmployeeService _removeEmployeeService;
        public EmployeeController(IAddEmployeeService addEmployeeService,IRemoveEmployeeService removeEmployeeService)
        {
            _employeeService = addEmployeeService;
            _removeEmployeeService = removeEmployeeService;
        }
        // POST api/<EmployeeController>
        [HttpPost]
        [Authorize(Policy = "BigAdminOrAdminOrSecretary")]
        public async Task<IActionResult> Post(AddEmployeeDto employee)
        {
            if(!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }
            var result = await _employeeService.Execute(employee);
            return Ok(result);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdminOrAdminOrSecretary")]
        public async Task<IActionResult> Delete(string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=await _removeEmployeeService.Execute(id);
            return Ok(result);
        }
    }
}
