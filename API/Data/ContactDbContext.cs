using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// Representation of the Contact DB
    /// </summary>
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options) 
        {   
        }

        public DbSet<ContactModel> Contacts { get; set; }
    }
}
