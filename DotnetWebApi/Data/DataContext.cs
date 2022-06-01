using DotnetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
