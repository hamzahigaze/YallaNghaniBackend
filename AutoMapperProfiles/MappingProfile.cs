using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AccountEntity = YallaNghani.Models.Account;
using ParentEntity = YallaNghani.Models.Parent;
using CommonModels = YallaNghani.Models.Common;
using TeacherEntity = YallaNghani.Models.Teacher;
using OfferedCourseEntity = YallaNghani.Models.OfferedCourse;

using AccountDtos = YallaNghani.DTOs.Accounts;

using ParentCourseDtos = YallaNghani.DTOs.Parents.Course;
using LessonDtos = YallaNghani.DTOs.Parents.Lesson;
using LessonDateDtos = YallaNghani.DTOs.Parents.LessonDate;
using MessageDtos = YallaNghani.DTOs.Parents.Message;
using ParentDtos = YallaNghani.DTOs.Parents.Parent;
using PaymentDtos = YallaNghani.DTOs.Parents.Payment;

using TeacherDtos = YallaNghani.DTOs.Teachers.Teacher;
using TeacherCourseDtos = YallaNghani.DTOs.Teachers.Course;

using OfferedCourseDtos = YallaNghani.DTOs.OfferedCourse;

namespace YallaNghani.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountEntity.Account, AccountDtos.ReadDto>();

            CreateMap<AccountEntity.Account, ParentDtos.ReadDto>();
            CreateMap<ParentEntity.Parent, ParentDtos.ReadDto>();

            CreateMap<AccountEntity.Account, TeacherDtos.ReadDto>();
            CreateMap<TeacherEntity.Teacher, TeacherDtos.ReadDto>();


            CreateMap<AccountDtos.CreateDto, AccountEntity.Account>();

            CreateMap<ParentCourseDtos.CreateDto, ParentEntity.Course>();
            CreateMap<ParentEntity.Course, ParentCourseDtos.ReadDto>();

            CreateMap<LessonDtos.CreateDto, ParentEntity.Lesson>();
            CreateMap<ParentEntity.Lesson, LessonDtos.ReadDto>();

            CreateMap<LessonDateDtos.CreateDto, CommonModels.LessonDate>();
            CreateMap<CommonModels.LessonDate, LessonDateDtos.ReadDto>();

            CreateMap<LessonDateDtos.CreateDto, CommonModels.LessonDate>();
            CreateMap<CommonModels.LessonDate, LessonDateDtos.CreateDto>();

            CreateMap<PaymentDtos.CreateDto, ParentEntity.Payment>();
            CreateMap<ParentEntity.Payment, PaymentDtos.ReadDto>();

            CreateMap<TeacherDtos.CreateDto, TeacherEntity.Teacher>();
            CreateMap<TeacherCourseDtos.CreateDto, TeacherEntity.Course>();

            CreateMap<MessageDtos.CreateDto, ParentEntity.Message>();

            CreateMap<OfferedCourseEntity.OfferedCourse, OfferedCourseDtos.ReadDto>();
            CreateMap<OfferedCourseDtos.CreateDto, OfferedCourseEntity.OfferedCourse>();

        }
    }
}
