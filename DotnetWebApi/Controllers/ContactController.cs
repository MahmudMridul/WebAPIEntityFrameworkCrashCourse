using DotnetWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private List<Contact> Contacts = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "Peter", LastName = "Parker", NickName = "SpiderMan", Place = "NY" },
            new Contact { Id = 2, FirstName = "Tony", LastName = "Stark", NickName = "IronMan", Place = "NY" },
        };

        // GET: api/<ContactController>
        [HttpGet]
        public ActionResult<IEnumerable<Contact>>  Get()
        {
            return Ok(Contacts);
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            Contact contact = Contacts.SingleOrDefault(contact => contact.Id == id);

            if(contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST api/<ContactController>
        [HttpPost]
        public ActionResult<IEnumerable<Contact>> AddContact(Contact NewContact)
        {
            Contacts.Add(NewContact);
            return Ok(Contacts);

        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
