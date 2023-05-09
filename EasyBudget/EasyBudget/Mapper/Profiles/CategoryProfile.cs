using AutoMapper;
using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Data.Models;

namespace EasyBudget.Mapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, ReadCategoryDto>()
                .ForMember(readCategoryDto => readCategoryDto.TypeName, options => options.MapFrom(category => category.Type.ToString()));
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
