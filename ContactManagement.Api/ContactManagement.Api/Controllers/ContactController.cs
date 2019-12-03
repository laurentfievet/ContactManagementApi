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
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {

            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contactService.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(await _contactService.GetByIdAsync(id));
        }

        [HttpPost, HttpPut]
        public async Task<IActionResult> PutOrPost([FromBody] ContactDTO contactDTO)
        {
            var item = await _contactService.SaveAsync(contactDTO);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }

        [HttpDelete("{contactId}")]
        public async Task<ActionResult<Contact>> Delete([FromRoute] long contactId)
        {
            await _contactService.DeleteAsync(contactId);
            return NoContent();
        }
    }
}
