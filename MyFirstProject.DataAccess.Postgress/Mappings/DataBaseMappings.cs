
using AutoMapper;
using MyFirstProject.Core.Models;
using MyFirstProject.DataAccess.Postgress.Entities;

namespace MyFirstProject.DataAccess.Postgress.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            //CreateMap<CourseEntity, Course>();
            //CreateMap<LessonEntity, Lesson>();
            CreateMap<UserEntity, User>();
        }
    }
}
