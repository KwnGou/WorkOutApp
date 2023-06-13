using AutoMapper;
using WorkOutAPI.Model;
using WorkOutDTOs;
using MyModel = WorkOutAPI.Model;


namespace WorkOutAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, Category_GridDTO>()
                .ReverseMap();

            CreateMap<Exercis, Exercise_GridDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => (src.Category == null) ? null : src.Category.Name));

            CreateMap<Exercis, Exercise_GridDTO>()
                .ReverseMap();

            CreateMap<Schedule, Schedule_GridDTO>()
                .ReverseMap();

            CreateMap<Schedule, Schedule_DetailsDTO>()
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.ScheduleDailyExercis));

            CreateMap<Schedule_DetailsDTO, Schedule>();

            CreateMap<ScheduleDailyExercis, Schedule_DailyExercise_ItemDTO>()
                .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.Name));
        }
    }
}
