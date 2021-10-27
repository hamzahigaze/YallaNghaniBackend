using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParentDtos = YallaNghani.DTOs.Parents.Parent;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using ParentEntity = YallaNghani.Models.Parent;
using CourseDtos = YallaNghani.DTOs.Parents.Course;
using LessonDtos = YallaNghani.DTOs.Parents.Lesson;
using PaymentDtos = YallaNghani.DTOs.Parents.Payment;
using MessageDtos = YallaNghani.DTOs.Parents.Message;
using LessonDateDtos = YallaNghani.DTOs.Parents.LessonDate;


namespace YallaNghani.Services.Parents
{
    public interface IParentsService
    {

        Task<ServiceResult<ParentDtos.ReadDto>> GetById(string id);

        Task<ServiceResult<PagedList<ParentDtos.ReadDto>>> Get(PaginationParameters paginationParameters);

        Task<ServiceResult<CourseDtos.ReadDto>> AddCourse(string id, CourseDtos.CreateDto dto);

        Task<ServiceResult> UpdateCourse(string id, string courseId, CourseDtos.UpdateDto dto);

        Task<ServiceResult> ChangeTeacher(String id, String courseId, CourseDtos.ChangeTeacherDto dto);

        Task<ServiceResult> DeleteCourse(string id, string courseId);

        Task<ServiceResult<LessonDtos.ReadDto>> AddLesson(string id, string courseId, LessonDtos.CreateDto dto);

        Task<ServiceResult> UpdateLesson(string id, string courseId, string lessonId, LessonDtos.UpdateDto dto);

        Task<ServiceResult> DeleteLesson(string id, string courseId, string LessonId);

        Task<ServiceResult> MarkLessonAsPaied(string id, string courseId, string lessonId);

        Task<ServiceResult<PaymentDtos.ReadDto>> AddPayment(string id, string courseId, PaymentDtos.CreateDto dto);

        Task<ServiceResult> DeletePayment(string id, string courseId, string paymentId);

        Task<ServiceResult<LessonDateDtos.ReadDto>> AddLessonDate(string id, string courseId, LessonDateDtos.CreateDto dto);

        Task<ServiceResult> DeleteLessonDate(string id, string courseId, string lessonDateId);

        Task<ServiceResult<string>> AddMessage(string id, MessageDtos.CreateDto dto);

        Task<ServiceResult> DeleteMessage(string id, string messageId);

        Task<ServiceResult> ResetNewMessagesCounter(string id);

        Task<ServiceResult> ConsumeLessonsDatesUpdate(string id);


    }
}
