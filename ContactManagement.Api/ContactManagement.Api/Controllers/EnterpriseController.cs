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
    /// <summary>
    /// Controller use for Get, Save, Update  enterprises
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ValidateModel]
    public class EnterpriseController : ControllerBase
    {

        private readonly IEnterpriseService _enterpriseService;

        /// <summary>
        /// Enterprise Constructor
        /// </summary>
        /// <param name="enterpriseService"></param>
        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        /// <summary>
        /// Get All enterprises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _enterpriseService.ListAsync());
        }

        /// <summary>
        /// Get Enterprise By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var item = await _enterpriseService.GetByIdAsync(id);
            if (item == null)
                return NotFound(id);

            return Ok(item);
        }

        /// <summary>
        /// Add or Update enterprise
        /// </summary>
        /// <param name="enterpriseDTO"></param>
        /// <returns></returns>
        [HttpPost, HttpPut]
        public async Task<IActionResult> PutOrPost([FromBody] EnterpriseDTOFull enterpriseDTO)
        {
            var item = await _enterpriseService.SaveAsync(enterpriseDTO);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }

        /// <summary>
        /// Add Addresses to an existing enterprise
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <param name="enterpriseAddressList"></param>
        /// <returns></returns>
        [ HttpPut("{enterpriseId}/addAddresses")]
        public async Task<IActionResult> addAddresses(long enterpriseId, [FromBody] EnterpriseAddresListDTO enterpriseAddressList)
        {
            var item = await _enterpriseService.AddAddressesAsync(enterpriseId, enterpriseAddressList.enterpriseAddresses);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }

        /// <summary>
        /// Delete existing enterprise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            await _enterpriseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
