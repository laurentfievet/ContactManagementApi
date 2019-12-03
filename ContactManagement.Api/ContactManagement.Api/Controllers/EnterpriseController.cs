using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContactManagement.Api.Validators;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using ContactManagement.Repo.Repositories;
using ContactManagement.Repo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ValidateModel]
    public class EnterpriseController : ControllerBase
    {

        private readonly IEnterpriseService _enterpriseService;

        public EnterpriseController(IEnterpriseService enterpriseService)
        {

            _enterpriseService = enterpriseService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _enterpriseService.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(await _enterpriseService.GetByIdAsync(id));
        }

        [HttpPost, HttpPut]
        public async Task<IActionResult> PutOrPost([FromBody] EnterpriseDTOFull enterpriseDTO)
        {
            var item = await _enterpriseService.SaveAsync(enterpriseDTO);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            await _enterpriseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
