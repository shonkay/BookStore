using AutoMapper;
using BookStore.Model;
using BookStore.Model.Dto;

namespace BookStore.API
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<Category, CreateOrUpdateCategoryDto>();
            CreateMap<CreateOrUpdateCategoryDto, Category>();
        }
    }
}
