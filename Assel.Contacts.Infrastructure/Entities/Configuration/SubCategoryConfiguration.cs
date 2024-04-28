using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Infrastructure.Entities.Configuration
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData
            (
                new SubCategory
                {
                    Id = new Guid("5607a79f-455f-4330-9d02-4c3a44159db3"),
                    Name = "Szef",
                    CategoryId = new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7")
                },
                new SubCategory
                {
                    Id = new Guid("7dacda57-2fd7-4b49-9ed5-df81be7c675a"),
                    Name = "Klient",
                    CategoryId = new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7")
                }
            );
        }

    }

}
