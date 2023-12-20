using Atelier.Application.Services.Users.Commands.AddUser;
using Atelier.Application.Services.Users.Commands.DeleteUser;
using Atelier.Application.Services.Users.Commands.EditUser;
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
        private readonly IEditEmployeeService _editEmployeeService;
        private readonly IRemoveEmployeeService _removeEmployeeService;
        public EmployeeController(IAddEmployeeService addEmployeeService,IEditEmployeeService editEmployeeService,IRemoveEmployeeService removeEmployeeService)
        {
            _employeeService = addEmployeeService;
            _editEmployeeService = editEmployeeService;
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

        // PUT api/<SecretaryController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdminOrAdminOrSecretary")]
        public async Task<IActionResult> Put(string id, [FromBody] EditEmployeeDto editEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _editEmployeeService.Execute(id, editByUserId, editEmployee);
            return Ok(result);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "BigAdminOrAdminOrSecretary")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var remByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result =await _removeEmployeeService.Execute(id, remByUserId);
            return Ok(result);
        }
    }
}
