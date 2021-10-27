using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParentDtos = YallaNghani.DTOs.Parents.Parent;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using YallaNghani.Repositories.Parent;
using ParentEntity = YallaNghani.Models.Parent;
using TeacherEntity = YallaNghani.Models.Teacher;
using AccountEntity = YallaNghani.Models.Account;
using CommonModels = YallaNghani.Models.Common;
using CourseDtos = YallaNghani.DTOs.Parents.Course;
using LessonDtos = YallaNghani.DTOs.Parents.Lesson;
using PaymentDtos = YallaNghani.DTOs.Parents.Payment;
using MessageDtos = YallaNghani.DTOs.Parents.Message;
using LessonDateDtos = YallaNghani.DTOs.Parents.LessonDate;
using TeacherDtos = YallaNghani.DTOs.Teachers.Teacher;
using YallaNghani.DTOs.Parents.Lesson;
using YallaNghani.Repositories.Teacher;
using YallaNghani.Repositories.Accounts;

namespace YallaNghani.Services.Parents
{
    public class ParentsService : IParentsService
    {
        private readonly IParentsRepository _parentsRepository;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;
        private readonly IAccountsRepository _accountsRepository;

        public ParentsService(IParentsRepository parentsRepository, IMapper mapper, 
                              ITeachersRepository teachersRepository, IAccountsRepository accountsRepository)
        {
            _parentsRepository = parentsRepository;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
            _accountsRepository = accountsRepository;
        }

        #region Parent

        public async Task<ServiceResult<PagedList<ParentDtos.ReadDto>>> Get(PaginationParameters paginationParameters)
        {
            var pagedParents = (await _parentsRepository.GetAsync(e => true, paginationParameters));          

            var parentsDtos = new List<ParentDtos.ReadDto>();

            foreach (var parent in pagedParents.Items) 
            {
                var account = await _accountsRepository.GetAsync(a => a.Id == parent.AccountId);

                var dto = _mapper.Map<ParentDtos.ReadDto>(account);
                dto = _mapper.Map<ParentEntity.Parent, ParentDtos.ReadDto>(parent, dto);

                parentsDtos.Add(dto);
            }                    

            var pagedDtos = parentsDtos.ToPagedList<ParentDtos.ReadDto>(parentsDtos.Count, paginationParameters.PageIndex, paginationParameters.PageSize);

            return ServiceResult<PagedList<ParentDtos.ReadDto>>.Success(pagedDtos);
        }

        public async Task<ServiceResult<ParentDtos.ReadDto>> GetById(string id)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult<ParentDtos.ReadDto>.NotFound("No parent was found with the given id");            

            var account = await _accountsRepository.GetAsync(a => a.Id == parent.AccountId);

            parent.Messages.Reverse();
            parent.Courses.Reverse();
            foreach (var course in parent.Courses)
            {
                course.Lessons.Reverse();
                course.Payments.Reverse();
            }

            var dto = _mapper.Map<ParentDtos.ReadDto>(account);
            dto = _mapper.Map<ParentEntity.Parent, ParentDtos.ReadDto>(parent, dto);

            return ServiceResult<ParentDtos.ReadDto>.Success(dto);
        }       

        #endregion Parent

        #region Course

        public async Task<ServiceResult<CourseDtos.ReadDto>> AddCourse(string id, CourseDtos.CreateDto dto)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult<CourseDtos.ReadDto>.NotFound("No parent was found with the given id");

            var teacher = await _teachersRepository.GetAsync(t => t.Id == dto.TeacherId);

            if (teacher == null)
                return ServiceResult<CourseDtos.ReadDto>.BadRequest("No teahcer was found with the given id");

            var course = _mapper.Map<ParentEntity.Course>(dto);

            var teacherAccounte = await _accountsRepository.GetAsync(a => a.Id == teacher.AccountId);

            course.TeacherName = $"{teacherAccounte.FirstName} {teacherAccounte.LastName}";

            parent.Courses.Add(course);

            teacher.Courses.Add(new TeacherEntity.Course { 
                Id = course.Id,
                ParentId = parent.Id,
                StudentName = dto.StudentName,
                Title = dto.Title,
                LessonsDates = course.LessonsDates
            });

            await _teachersRepository.UpdateAsync(teacher);
            await _parentsRepository.UpdateAsync(parent);

            var readDto = _mapper.Map<CourseDtos.ReadDto>(course);

            return ServiceResult<CourseDtos.ReadDto>.Success(readDto);
        }

        public async Task<ServiceResult> UpdateCourse(string id, string courseId, CourseDtos.UpdateDto dto)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult<string>.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            var teacher = await _teachersRepository.GetAsync(t => t.Id == course.TeacherId);

            if (teacher == null)
                return ServiceResult.InternalServerError("Data Error: The teacher id in the course referes to null");

            var teacherCourse = teacher.Courses.FirstOrDefault(c => c.Id == course.Id);

            if (teacherCourse == null)
                return ServiceResult.InternalServerError("Data Error: The teacher that is linked with the course, doesn't have a course with the given id");

            var dtoProperties = dto.GetType().GetProperties().ToList();

            dtoProperties.ForEach((property) =>
            {

                var propertyName = property.Name;
                var propertyValue = property.GetValue(dto);

                if (propertyValue != null)
                    course.GetType().GetProperty(propertyName).SetValue(course, propertyValue);

            });

            teacherCourse.StudentName = course.StudentName;
            teacherCourse.Title = course.Title;

            await _teachersRepository.UpdateAsync(teacher);
            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ChangeTeacher(String id, String courseId, CourseDtos.ChangeTeacherDto dto) {

            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var newTeacher = await _teachersRepository.GetAsync(t => t.Id == dto.newTeacherId);

            if (newTeacher == null)
                return ServiceResult.NotFound("No Teacher Was Found With The Given Id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            if (course.TeacherId == dto.newTeacherId)
                return ServiceResult.BadRequest("The TeacherId of the course is the same as newTeacherId");

            var previousTeacher = await _teachersRepository.GetAsync(t => t.Id == course.TeacherId);

            if(previousTeacher == null)
                return ServiceResult.NotFound("Data Error: The teacher id in the course referes to null");


            var teacherCourse = previousTeacher.Courses.Find(c => c.Id == course.Id);            

            if(teacherCourse == null)
                return ServiceResult.NotFound("Data Error: The teacher that is linked with the course, doesn't have a course with the given id");

            previousTeacher.Courses.Remove(teacherCourse);
            course.TeacherId = dto.newTeacherId;

            var newTeacherAccount = await _accountsRepository.GetAsync(t => t.Id == newTeacher.AccountId);
            course.TeacherName = $"{newTeacherAccount.FirstName} {newTeacherAccount.LastName}";
            newTeacher.Courses.Add(teacherCourse);

            await _parentsRepository.UpdateAsync(parent);
            await _teachersRepository.UpdateAsync(previousTeacher);
            await _teachersRepository.UpdateAsync(newTeacher);
            
        return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteCourse(string id, string CourseId)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == CourseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            var teacher = await _teachersRepository.GetAsync(t => t.Id == course.TeacherId);

            if (teacher == null)
                return ServiceResult.InternalServerError("Data Error: The teacher id in the course referes to null");

            teacher.Courses.RemoveAll(c => c.Id == CourseId);
            parent.Courses.RemoveAll(c => c.Id == CourseId);

            await _teachersRepository.UpdateAsync(teacher);
            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        #endregion

        #region Lesson

        public async Task<ServiceResult<LessonDtos.ReadDto>> AddLesson(string id, string courseId, LessonDtos.CreateDto dto)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult<LessonDtos.ReadDto>.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult<LessonDtos.ReadDto>.NotFound("No course was found with the given id");

            var lesson = _mapper.Map<ParentEntity.Lesson>(dto);

            lesson.Number = course.Lessons.Count() == 0 ? 1 : course.Lessons.Max(p => p.Number) + 1;
            lesson.Fee = course.LessonFee;
            lesson.PaymentStatus = ParentEntity.PaymentStatus.UnPaied;
            
            course.Lessons.Add(lesson);

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult<LessonDtos.ReadDto>.Success(_mapper.Map<LessonDtos.ReadDto>(lesson));
        }

        public async Task<ServiceResult> UpdateLesson(string id, string courseId, string lessonId, LessonDtos.UpdateDto dto)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            var lesson = course.Lessons.FirstOrDefault(l => l.Id == lessonId);

            if (lesson == null)
                return ServiceResult.NotFound("No lesson was found with the given id");

            var dtoProperties = dto.GetType().GetProperties().ToList();

            dtoProperties.ForEach((property) =>
            {

                var propertyName = property.Name;
                var propertyValue = property.GetValue(dto);

                if (propertyValue != null)
                    lesson.GetType().GetProperty(propertyName).SetValue(lesson, propertyValue);

            });

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();

        }

        public async Task<ServiceResult> DeleteLesson(string id, string courseId, string lessonId)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            var lesson = course.Lessons.FirstOrDefault(l => l.Id == lessonId);

            if (lesson == null)
                return ServiceResult.NotFound("No lesson was found with the given id");

            course.Lessons.Remove(lesson);

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> MarkLessonAsPaied(string id, string courseId, string lessonId)
        {
            var parent = await _parentsRepository.GetAsync(p => p.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            var lesson = course.Lessons.FirstOrDefault(l => l.Id == lessonId);

            if (lesson == null)
                return ServiceResult.NotFound("No lesson was found with the given id");

            lesson.PaymentStatus = ParentEntity.PaymentStatus.Paied;
            course.ParentBalance -= lesson.Fee;

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        #endregion

        #region Payment

        public async Task<ServiceResult<PaymentDtos.ReadDto>> AddPayment(string id, string courseId, PaymentDtos.CreateDto dto)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult<PaymentDtos.ReadDto>.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult<PaymentDtos.ReadDto>.NotFound("No course was found with the given id");

            var payment = _mapper.Map<ParentEntity.Payment>(dto);

            course.ParentBalance += payment.Amount;

            course.Payments.Add(payment);

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult<PaymentDtos.ReadDto>.Success(_mapper.Map<PaymentDtos.ReadDto>(payment));
        }

        public async Task<ServiceResult> DeletePayment(string id, string courseId, string paymentId)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

            var payment = course.Payments.FirstOrDefault(p => p.Id == paymentId);

            if (payment == null)
                return ServiceResult.NotFound("No payment was found with the given id");
           
            course.ParentBalance -= payment.Amount;

            course.Payments.Remove(payment);

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        #endregion

        #region Message

        public async Task<ServiceResult<string>> AddMessage(string id, MessageDtos.CreateDto dto) 
        {
            var parent = await _parentsRepository.GetAsync(p => p.Id == id);

            if (parent == null)
                return ServiceResult<string>.NotFound("No parent was found with the given id");

            var message = _mapper.Map<ParentEntity.Message>(dto);

            parent.Messages.Add(message);

            parent.NewMessagesCount += 1;

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult<string>.Success(message.Id);
        }

        public async Task<ServiceResult> DeleteMessage(string id, string messageId)
        {
            var parent = await _parentsRepository.GetAsync(p => p.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var removeCount = parent.Messages.RemoveAll(m => m.Id == messageId);

            if (removeCount != 1)
                return ServiceResult.NotFound("No message was found with the given id");

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ResetNewMessagesCounter(string id)
        {
            var parent = await _parentsRepository.GetAsync(p => p.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            parent.NewMessagesCount = 0;

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        #endregion

        #region LessonDate

        public async Task<ServiceResult<LessonDateDtos.ReadDto>> AddLessonDate(string id, string courseId, LessonDateDtos.CreateDto dto)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult<LessonDateDtos.ReadDto>.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult<LessonDateDtos.ReadDto>.NotFound("No course was found with the given id");

            var lessonDate = _mapper.Map<CommonModels.LessonDate>(dto);

            course.LessonsDates.Add(lessonDate);

            var teacher = await _teachersRepository.GetAsync(t => t.Id == course.TeacherId);

            if (teacher == null)
                return ServiceResult<LessonDateDtos.ReadDto>.InternalServerError("The Developer Failed To Keep Data Integrity");

            var isValidHour = true;

            dto.Hour = dto.Hour.Trim();

            var hour = dto.Hour;

            if (hour.Length != 5)
                isValidHour = false;

            if (isValidHour && hour.Count(c => c == '.') == 1)
            {
                var splited = hour.Split('.');

                if (splited[0].Length != 2 && splited[1].Length != 2)
                    isValidHour = false;

                int x;

                if (isValidHour && int.TryParse(splited[0], out x))
                {
                    if (! (x >= 0 && x <= 23))
                        isValidHour = false;
                }
                else
                {
                    isValidHour = false;
                }

                if (isValidHour && int.TryParse(splited[1], out x))
                {
                    if (!(x >= 0 && x <= 59))
                        isValidHour = false;
                }
                else
                {
                    isValidHour = false;
                }

            }
            else
                isValidHour = false;

            if (!isValidHour)
                return ServiceResult<LessonDateDtos.ReadDto>.BadRequest("Unvalid hour fromat. Hour should be in the format: hh.mm");

            teacher.Courses.FirstOrDefault(c => c.Id == course.Id)?.LessonsDates.Add(lessonDate);
            parent.HasLessonsDatesUpdate = true;
            await _teachersRepository.UpdateAsync(teacher);
            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult<LessonDateDtos.ReadDto>.Success(_mapper.Map<LessonDateDtos.ReadDto>(lessonDate));
        }

        public async Task<ServiceResult> DeleteLessonDate(string id, string courseId, string lessonDateId)
        {
            var parent = await _parentsRepository.GetAsync(e => e.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            var course = parent.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
                return ServiceResult.NotFound("No course was found with the given id");

           var lessonDate = course.LessonsDates.FirstOrDefault(l => l.Id == lessonDateId);

            var teacher = await _teachersRepository.GetAsync(t => t.Id == course.TeacherId);

            if (teacher == null)
                return ServiceResult<string>.InternalServerError("The Developer Failed To Keep Data Integrity");

            var teacherCourse = teacher.Courses.FirstOrDefault(c => c.Id == courseId);

            if(teacherCourse == null)
                return ServiceResult<string>.InternalServerError("The Developer Failed To Keep Data Integrity");

            var teacherLessonDate = teacherCourse.LessonsDates.FirstOrDefault(l => l.Id == lessonDateId);

            if(lessonDate == null || teacherLessonDate == null)
                 return ServiceResult.NotFound("NO LessonDate Was Found With The Given Id");

            teacherCourse.LessonsDates.Remove(teacherLessonDate);

            course.LessonsDates.Remove(lessonDate);
            parent.HasLessonsDatesUpdate = true;
            await _parentsRepository.UpdateAsync(parent);
            await _teachersRepository.UpdateAsync(teacher);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ConsumeLessonsDatesUpdate(string id)
        {
            var parent = await _parentsRepository.GetAsync(p => p.Id == id);

            if (parent == null)
                return ServiceResult.NotFound("No parent was found with the given id");

            parent.HasLessonsDatesUpdate = false;

            await _parentsRepository.UpdateAsync(parent);

            return ServiceResult.Success();
        }

        #endregion
    }
}
