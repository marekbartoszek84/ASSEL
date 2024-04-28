using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Infrastructure.Entities.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .HasIndex(c => c.Email)
                .IsUnique();

            builder.HasData
            (
                new Contact
                {
                    Id = new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7"),
                    FirstName = "Ted",
                    LastName = "Buzz",
                    Email = "ted@wp.pl",
                    DateOfBirth = DateTime.Now.AddYears(-22),
                    Password = "1234",
                    Phone = "700700700",
                    CategoryId = new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7")
                },
                new Contact
                {
                    Id = new Guid("7c24fd27-34dd-4d51-b24f-e89b5f636b18"),
                    FirstName = "Faris",
                    LastName = "Bakala",
                    Email = "bakala@wp.pl",
                    DateOfBirth = DateTime.Now.AddYears(-40),
                    Password = "gjsdgjdsg",
                    Phone = "600706500",
                    CategoryId = new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7")
                }
            );
        }
    }
}
