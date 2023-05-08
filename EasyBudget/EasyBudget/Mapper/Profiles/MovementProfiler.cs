using AutoMapper;
using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Data.Models;

namespace EasyBudget.Mapper.Profiles
{
    public class MovementProfiler : Profile
    {
        public MovementProfiler()
        {
            CreateMap<CreateMovementDto, Movement>();
            CreateMap<Movement, ReadMovementDto>()
                .ForMember(readMovementDto => readMovementDto.Category, option => option.MapFrom(movement => movement.Category))
                .ForMember(readMovementDto => readMovementDto.Date, option => option.MapFrom(movement => DateOnly.FromDateTime(movement.Date)));
            CreateMap<UpdateMovementDto, Movement>();
        }
    }
}
