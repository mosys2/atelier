using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Ateliers.Commands;
using Atelier.Application.Services.Ateliers.Commands.ChangeStatusAtelier;
using Atelier.Application.Services.Ateliers.Commands.EditAtelier;
using Atelier.Application.Services.Ateliers.Commands.RemoveAtelier;
using Atelier.Application.Services.Ateliers.Queries;
using Atelier.Application.Services.Ateliers.Queries.GetDetailAtelier;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtelierBaseController : ControllerBase
    {
        private readonly IAtelierFacad _atelierFacad;
        public AtelierBaseController(IAtelierFacad atelierFacad)
        {
            _atelierFacad = atelierFacad;
        }
       
        // GET: api/<AtelierBaseController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            var result = await _atelierFacad.GetAllAtelierBase.Excute();
            return Ok(result);
        }
        // GET api/<UsersController>/5
        [HttpGet("Detail/{atelierId}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> GetDetail(string atelierId)
        {
            if (string.IsNullOrEmpty(atelierId))
            {
                return NotFound(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.NotFind
                });
            }
            var result = await _atelierFacad.GetDetailAtelierService.Execute(atelierId);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post(RequestAddAtelierDto atelier)
        {
            if (!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }
            var userId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _atelierFacad.AddAtelierService.Execute
                (
                new AddAtelierDto
                {
                    CurrentUserId=userId,
                    Description=atelier.Description,
                    Name=atelier.Name,
                    Status = atelier.Status,
                    StatusMessage = atelier.StatusMessage
                }
                );
            return Ok(result);
        }
        [HttpPost("ChangeStatus/{atelierId}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> ChangeStatus(string atelierId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _atelierFacad.ChangeStatusAtelierService.Execute(atelierId);
            return Ok(result);
        }
        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditAtelierDto editAtelier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editByUserId = User.Claims.First(u => u.Type == "UserId").Value;
            var result = await _atelierFacad.EditAtelierService.Execute(id, editByUserId, editAtelier);
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
            var result = await _atelierFacad.RemoveAtelierService.Execute(id, remByUserId);
            return Ok(result);
        }

    }
}
