using Assel.Contacts.Domain.Entities;
using Assel.Contacts.Domain.Models;
using AutoMapper;

namespace Assel.Contacts.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<Contact, ContactRequest>();
            CreateMap<Contact, ContactListResponse>();
            CreateMap<Contact, ContactResponse>();
            CreateMap<ContactRequest, Contact>()
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategoryRequest));
            CreateMap<SubCategoryRequest, SubCategory>();
            CreateMap<SubCategory, SubCategoryRequest>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<SubCategory, SubCategoryResponse>();
        }
    }
}
