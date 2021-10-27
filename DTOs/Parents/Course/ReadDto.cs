using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParentEntity = YallaNghani.Models.Parent;
using CommonModels = YallaNghani.Models.Common;
namespace YallaNghani.DTOs.Parents.Course
{
    public class ReadDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string StudentName { get; set; }

        public string  TeacherId { get; set; }

        public String  TeacherName { get; set; }

        public double LessonFee { get; set; }

        public double ParentBalance { get; set; }

        public List<ParentEntity.Lesson> Lessons { get; set; }

        public List<ParentEntity.Payment> Payments { get; set; }

        public List<CommonModels.LessonDate> LessonsDates { get; set; }

        public List<ParentEntity.Message> Messages { get; set; }

    }
}
