using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course_DTO, Course>();
            CreateMap<Lecturer_DTO, Lecturer>();
        }
    }
}
