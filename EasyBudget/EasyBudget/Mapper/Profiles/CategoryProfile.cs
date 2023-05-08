using AutoMapper;
using EasyBudget.Data.Dto.Category;
using EasyBudget.Data.Models;

namespace EasyBudget.Mapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, ReadCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
