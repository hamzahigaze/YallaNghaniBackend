using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Common;

namespace YallaNghani.DTOs.Teachers.Course
{
    public class ReadSingleDto
    {
        public string Id { get; set; }

        public string StudentName { get; set; }

        public string CourseTitle { get; set; }

        public string ParentId { get; set; }

        public IEnumerable<LessonDate> LessonsDates { get; set; }
    }
}
