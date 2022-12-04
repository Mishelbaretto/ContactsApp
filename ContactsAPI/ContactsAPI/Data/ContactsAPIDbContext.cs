using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
    //this is a class file which talks to the db
    //this class we will inherit it from DbContext
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        //in dbcontext we have to create properties, which will act like tables for entity framework core
        //bcs we need only 1 property which is our Contacts Model and we only want to create update retrieve and delete
        //so we will have dbset property of type Contact
        public DbSet<Contact> Contacts { get; set; }
        //we have to inject ContactsAPIDbContext into the services of the solution and we will use dependency injection to inject that injected service into our controller
        //so we can talk to our db


    }
}
