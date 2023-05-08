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
            CreateMap<Movement, ReadMovementDto>();
            CreateMap<UpdateMovementDto, Movement>();
        }
    }
}
