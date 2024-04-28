using Assel.Contacts.Repository.Entities;
using Assel.Contacts.Repository.Entities.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Repository
{
    public class ContactDbContext : IdentityDbContext<User>
    {
        public ContactDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
        }

        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<SubCategory>? SubCategories { get; set; }
    }
}
