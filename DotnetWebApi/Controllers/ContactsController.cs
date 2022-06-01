using DotnetWebApi.Data;
using DotnetWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly DataContext _context;

        public ContactsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.Where(c => c.IsDeleted == false).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            Contact FoundContact = await _context.Contacts.FindAsync(id);

            if (FoundContact == null) { return NotFound("Not Found"); }
            if (FoundContact.IsDeleted == true) { return NotFound("Contact was deleted"); }

            return FoundContact;
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> AddContact(Contact NewContact)
        {
            _context.Contacts.Add(NewContact);
            await _context.SaveChangesAsync();

            return NewContact;
        }

        [HttpPut("placeupdate")]
        public async Task<ActionResult<Contact>> UpdatePlace(int id, string NewPlace)
        {
            Contact OldContact = await _context.Contacts.FindAsync(id);

            if (OldContact == null) { return NotFound("Not Found"); }
            if (OldContact.IsDeleted == true) { return NotFound("Contact was deleted"); }

            OldContact.Place = NewPlace;
            await _context.SaveChangesAsync();

            return OldContact;
        }

        [HttpPut("recover")]
        public async Task<ActionResult<Contact>> RecoverContact(int id)
        {
            Contact FoundContact = await _context.Contacts.FindAsync(id);

            if (FoundContact == null) { return NotFound("Not Found"); }
            if (FoundContact.IsDeleted == true) 
            {
                FoundContact.IsDeleted = false;
                await _context.SaveChangesAsync();
            }

            return FoundContact;
        }

        [HttpDelete("deleteforever")]
        public async Task<ActionResult<Contact>> DeleteContactPerm(int id)
        {
            Contact FoundContact = await _context.Contacts.FindAsync(id);

            if (FoundContact == null) { return NotFound("Not Found"); }
            if (FoundContact.IsDeleted == true) { return NotFound("Contact was deleted"); }

            _context.Contacts.Remove(FoundContact);
            await _context.SaveChangesAsync();

            return FoundContact;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Contact>> DeleteContactTemp(int id)
        {
            Contact FoundContact = await _context.Contacts.FindAsync(id);

            if (FoundContact == null) { return NotFound("Not Found"); }
            if (FoundContact.IsDeleted == true) { return NotFound("Contact was deleted"); }

            FoundContact.IsDeleted = true;
            await _context.SaveChangesAsync();

            return FoundContact;
        }

    }
}
