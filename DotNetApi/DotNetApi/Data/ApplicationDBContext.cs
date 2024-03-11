using Microsoft.EntityFrameworkCore;
using DotNetApi.Model;

namespace DotNetApi.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
               
        }

        public DbSet<User> users { get; set; }

        public DbSet<Contact> contacts { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            base.OnModelCreating(modelBuilder);
        }
    }
}
