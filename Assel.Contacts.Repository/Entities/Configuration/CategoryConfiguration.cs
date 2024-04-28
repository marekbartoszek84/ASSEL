using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Repository.Entities.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = new Guid("e385f467-b1ee-4df9-86ba-c91c5e3dccb7"),
                    Name = "Sluzbowy"
                },
                new Category
                {
                    Id = new Guid("7c24fd27-34dd-4d51-b24f-e89b5f636b18"),
                    Name = "Prywatny"
                },
                new Category
                {
                    Id = new Guid("db9d8f23-1202-436b-9eae-49aa590a1e2a"),
                    Name = "Inny"
                }
            );
        }
    }
}
