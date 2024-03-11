using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetApi.Data;
using DotNetApi.Model;
using Mapster;

namespace DotNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private IHttpContextAccessor _contextAccessor;

        public ContactsController(ApplicationDBContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> Getcontacts()
        {
            User user = (User)_contextAccessor.HttpContext.Items["User"];

            if (_context.contacts == null)
          {
              return NotFound();
          }
            var ContactList = await _context.contacts.Where(x => x.UserId == user.Id).ToListAsync();
            
            if(ContactList==null)
            return new List<ContactDTO>();

            return ContactList.Adapt<List<ContactDTO>>();


        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetContact(Guid id)
        {
          if (_context.contacts == null)
          {
              return NotFound();
          }
            var contact = await _context.contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact.Adapt<ContactDTO>();
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(Guid id, ContactDTO contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var contacts = await _context.contacts.FindAsync(id);
            contacts.Email = contact.Email;
            contacts.PhoneNumber = contact.PhoneNumber;
            contacts.Name = contact.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(ContactDTO contact)
        {
          if (_context.contacts == null)
          {
              return Problem("Entity set 'ApplicationDBContext.contacts'  is null.");
          }
            User user = (User)_contextAccessor.HttpContext.Items["User"];
            contact.UserId = user.Id;
            _context.contacts.Add(contact.Adapt<Contact>());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            if (_context.contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(Guid id)
        {
            return (_context.contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
