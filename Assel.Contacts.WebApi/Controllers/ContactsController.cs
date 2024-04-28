using Assel.Contacts.WebApi.Models;
using Assel.Contacts.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assel.Contacts.WebApi.Extensions;

namespace Assel.Contacts.WebApi.Controllers
{
    [Route("api/contacts")]
    [Authorize]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _contactService.GetAllAsync();

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var result = await _contactService.GetAsync(id);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ContactRequest contactRequest)
        {
            var result = await _contactService.AddAsync(contactRequest);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, ContactRequest contactRequest)
        {
            var result = await _contactService.UpdateAsync(id, contactRequest);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await _contactService.DeleteAsync(id);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }
    }
}
