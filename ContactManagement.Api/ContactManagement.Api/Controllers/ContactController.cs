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
    /// Controller use for Get, Save, Update contacts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ValidateModel]
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;

        /// <summary>
        /// Contact Constructor
        /// </summary>
        /// <param name="contactService"></param>
        public ContactController(IContactService contactService)
        {

            _contactService = contactService;
        }

        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contactService.ListAsync());
        }

        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(await _contactService.GetByIdAsync(id));
        }

        /// <summary>
        /// Add or Update a contact
        /// </summary>
        /// <param name="contactDTO"></param>
        /// <returns></returns>
        [HttpPost, HttpPut]
        public async Task<IActionResult> PutOrPost([FromBody] ContactDTO contactDTO)
        {
            var item = await _contactService.SaveAsync(contactDTO);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }

        /// <summary>
        /// Delete existing contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            await _contactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
