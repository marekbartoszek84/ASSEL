﻿namespace Assel.Contacts.Repository.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<SubCategory>? SubCategories { get; set; }
    }

}