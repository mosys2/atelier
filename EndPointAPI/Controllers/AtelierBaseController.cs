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
        private readonly IGetAllAtelierBase _getAllAtelierBase;
        private readonly IAddAtelierService _addAtelierService;
        private readonly IRemoveAtelierService _removeAtelierService;
        private readonly IGetDetailAtelierService _getDetailAtelierService;
        private readonly IEditAtelierService _editAtelierService;
        private readonly IChangeStatusAtelierService _changeStatusAtelierService;
        public AtelierBaseController
        (IGetAllAtelierBase getAllAtelierBase,
            IAddAtelierService addAtelierService,
            IRemoveAtelierService removeAtelierService,
            IGetDetailAtelierService getDetailAtelierService,
            IEditAtelierService editAtelierService,
            IChangeStatusAtelierService changeStatusAtelierService
        )
        {
            _getAllAtelierBase=getAllAtelierBase;
            _addAtelierService=addAtelierService;
            _removeAtelierService = removeAtelierService;
            _getDetailAtelierService=getDetailAtelierService;
            _editAtelierService=editAtelierService;
            _changeStatusAtelierService=changeStatusAtelierService;
        }
       
        // GET: api/<AtelierBaseController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            var result = await _getAllAtelierBase.Excute();
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
            var result = await _getDetailAtelierService.Execute(atelierId);
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
            var result = await _addAtelierService.Execute
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
            var result = await _changeStatusAtelierService.Execute(atelierId);
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
            var result = await _editAtelierService.Execute(id, editByUserId, editAtelier);
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
            var result = await _removeAtelierService.Execute(id, remByUserId);
            return Ok(result);
        }

    }
}
