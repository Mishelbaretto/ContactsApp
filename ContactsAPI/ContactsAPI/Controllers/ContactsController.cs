using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;

namespace ContactsAPI.Controllers
{
    [ApiController]//annotate it with keyword ApiController,telling that it is an api controller and not an mvc controller
    [Route("api/contacts")]// or also we can use ("api/[controller]")
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;//we will private readonly field to talk to our in memory database

        //we want to inject the dbcontext bcs we want to talk to our inmemory database
        public ContactsController(ContactsAPIDbContext contactsDbContext)//shortcut to create constructor is ctor( then twice tab)
        {
            this.dbContext = contactsDbContext;
        }

        [HttpGet]//by default asp.net core is auto matically identifying this as a get method, but for us to use swagger ui,its open documentation we have to annotate this wit httpget
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());//tells talk to the Contacts table and return a list..........we cant directly return either we have to wrap it inside Ok statement to give it a 200 response bcs it is a restfull api or IEnumerable like we had in the weather forecast api
             
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetConact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)//bcs we are using an async keyword the IactionResult has to be wrapped in the task
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,

            };
           await dbContext.Contacts.AddAsync(contact);//with entity framework core we also have to save the changes to the db
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContactRequest)
        {
           var contact= await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
    }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
           var contact= await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

    }
}
