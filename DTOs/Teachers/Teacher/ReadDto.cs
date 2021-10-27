using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherEntity = YallaNghani.Models.Teacher;

namespace YallaNghani.DTOs.Teachers.Teacher
{
    public class ReadDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<TeacherEntity.Course> Courses { get; set; }
    }
}
