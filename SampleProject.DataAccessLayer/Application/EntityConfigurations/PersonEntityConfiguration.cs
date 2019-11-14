using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleProject.DomainObject.Application;

namespace SampleProject.DataAccessLayer.Application.EntityConfigurations
{
    public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.UpdatedBy)
                .HasMaxLength(50);
        }
    }
}