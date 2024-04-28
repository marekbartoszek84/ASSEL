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

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _contactService.GetAll();

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var result = _contactService.GetDetails(id);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpPost]
        public IActionResult Add(ContactRequest contactRequest)
        {
            var result = _contactService.Add(contactRequest);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, ContactRequest contactRequest)
        {
            var result = _contactService.Update(id, contactRequest);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _contactService.Delete(id);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }
    }
}
