using Amazon.Auth.AccessControlPolicy;
using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Services.Ateliers.FacadPattern;
using Atelier.Common.Dto;
using EndPointAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EndPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageFacad _pageFacad;
        private readonly Guid userId;
        public PagesController(IPageFacad pageFacad, ClaimsPrincipal user)
        {
            _pageFacad = pageFacad;
            userId = Guid.Parse(ClaimUtility.GetUserId(user) ?? "");
        }
        // POST api/<BigAdminController>
        [HttpPost]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Post([FromBody] RequestPageDto page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pageFacad.AddPageService.Execute(page, userId);
            return Ok(result);
        }
        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Put(string id, [FromBody] EditPageDto editPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pageFacad.EditPageService.Execute(id, userId, editPage);
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
            var result = await _pageFacad.RemovePageService.Execute(id, userId);
            return Ok(result);
        }
        // GET: api/<AtelierBaseController>
        [HttpGet]
        [Authorize(Policy = "BigAdmin")]
        public async Task<IActionResult> Get()
        {
            var result = await _pageFacad.GetAllPageService.Excute();
            return Ok(result);
        }
    }

}
