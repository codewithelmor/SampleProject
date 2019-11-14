using Microsoft.EntityFrameworkCore;
using SampleProject.DataAccessLayer.Application.EntityConfigurations;
using SampleProject.DomainObject.Application;

namespace SampleProject.DataAccessLayer.Application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        }

        public DbSet<Person> Persons { get; set; }
    }
}
