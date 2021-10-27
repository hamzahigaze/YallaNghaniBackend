using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParentDtos = YallaNghani.DTOs.Parents.Parent;
using CourseDtos = YallaNghani.DTOs.Parents.Course;
using LessonDtos = YallaNghani.DTOs.Parents.Lesson;
using PaymentDtos = YallaNghani.DTOs.Parents.Payment;
using MessageDtos = YallaNghani.DTOs.Parents.Message;
using LessonDateDtos = YallaNghani.DTOs.Parents.LessonDate;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using YallaNghani.Services.Parents;
using Microsoft.AspNetCore.Authorization;

namespace YallaNghani.Controllers
{
    [ApiController()]
    [Route("[controller]")]
    //[Produces("application/json")]
    public class ParentsController : Controller
    {

        private readonly IParentsService _parentsService;

        public ParentsController(IParentsService parentsService)
        {
            _parentsService = parentsService;
        }

        #region Parent Profile        

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParemeters)
        {
            return _mapResult(await _parentsService.Get(paginationParemeters));
        }

        [Authorize(Roles = "Admin,Parent")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        { 
            var result = await _parentsService.GetById(id);

            return _mapResult(result);
        }

        #endregion

        #region Course

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/courses")]
        public async Task<IActionResult> AddCourse(string id, [FromBody] CourseDtos.CreateDto dto)
        {
            dto.LessonFee = Convert.ToDouble(dto.LessonFee);
            return _mapResult(await _parentsService.AddCourse(id, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/courses/{courseId}")]
        public async Task<IActionResult> UpdateCourse(string id, string courseId, [FromBody] CourseDtos.UpdateDto dto)
        {
            if (dto.LessonFee != null)
                dto.LessonFee = Convert.ToDouble(dto.LessonFee);
            return _mapResult(await _parentsService.UpdateCourse(id, courseId, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/courses/{courseId}/changeteacher")]
        public async Task<IActionResult> ChangeTeacher(string id, string courseId, [FromBody] CourseDtos.ChangeTeacherDto dto)
        {
       
            return _mapResult(await _parentsService.ChangeTeacher(id, courseId, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/courses/{courseId}")]
        public async Task<IActionResult> DeleteCourse(string id, string courseId)
        {
            return _mapResult(await _parentsService.DeleteCourse(id, courseId));
        }

        #endregion

        #region Lesson

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/courses/{courseId}/lessons")]
        public async Task<IActionResult> AddLesson(string id, string courseId, [FromBody] LessonDtos.CreateDto dto)
        {
            return _mapResult(await _parentsService.AddLesson(id, courseId, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/courses/{courseId}/lessons/{lessonId}")]
        public async Task<IActionResult> UpdateLesson(string id, string courseId, string lessonId, [FromBody] LessonDtos.UpdateDto dto)
        {
            return _mapResult(await _parentsService.UpdateLesson(id, courseId, lessonId, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/courses/{courseId}/lessons/{lessonId}")]
        public async Task<IActionResult> DeleteLesson(string id, string courseId, string lessonId)
        {
            return _mapResult(await _parentsService.DeleteLesson(id, courseId, lessonId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/courses/{courseId}/lessons/{lessonId}/pay")]
        public async Task<IActionResult> PayLesson(string id, string courseId, string lessonId)
        {
            return _mapResult(await _parentsService.MarkLessonAsPaied(id, courseId, lessonId));
        }

        #endregion

        #region Payment

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/courses/{courseId}/payments")]
        public async Task<IActionResult> AddPayment(string id, string courseId, [FromBody] PaymentDtos.CreateDto dto)
        {
            dto.Amount = Convert.ToDouble(dto.Amount);
            return _mapResult(await _parentsService.AddPayment(id, courseId, dto));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/courses/{courseId}/payments/{paymentId}")]
        public async Task<IActionResult> DeletePayment(string id, string courseId, string paymentId)
        {
            return _mapResult(await _parentsService.DeletePayment(id, courseId, paymentId));
        }

        #endregion

        #region Lessons Dates

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/courses/{courseId}/lessonsdates")]
        public async Task<IActionResult> AddLessonDate(string id, string courseId, [FromBody] LessonDateDtos.CreateDto dto)
        {
            return _mapResult(await _parentsService.AddLessonDate(id, courseId, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/courses/{courseId}/lessonsdates/{lessonDateId}")]
        public async Task<IActionResult> DeleteLessonDate(string id, string courseId, string lessonDateId)
        {
            return _mapResult(await _parentsService.DeleteLessonDate(id, courseId, lessonDateId));
        }

        [Authorize(Roles = "Parent")]
        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> ConsumeLessonsDatesUpdate(string id)
        {
            return _mapResult(await _parentsService.ConsumeLessonsDatesUpdate(id));
        }
        #endregion

        #region Messages

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost("{id}/messages")]
        public async Task<IActionResult> AddMessage(string id, [FromBody] MessageDtos.CreateDto dto)
        {
            return _mapResult(await _parentsService.AddMessage(id, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/messages/{messageId}")]
        public async Task<IActionResult> DeleteMessage(string id, string messageId)
        {
            return _mapResult(await _parentsService.DeleteMessage(id, messageId));
        }

        [Authorize(Roles = "Parent")]
        [HttpPost("{id}/resetnewmessagescount")]
        public async Task<IActionResult> ResetNewMessagesCounter(string id)
        {
            return _mapResult(await _parentsService.ResetNewMessagesCounter(id));
        }


        #endregion

        private IActionResult _mapResult(ServiceResult result)
        {
            if (result.StatusCode == 200)
                return Ok(result);

            else if (result.StatusCode == 400)
                return BadRequest(result);

            else if (result.StatusCode == 404)
                return NotFound(result);

            else if (result.StatusCode == 403)
                return Forbid();

            return BadRequest(result);
        }
    }
}
