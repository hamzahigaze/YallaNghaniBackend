using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.OfferedCourse
{
    public class ReadDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string TeacherName { get; set; }

        public string TeacherDescription { get; set; }

        public bool IsEducational { get; set; }

        public string ImageUrl { get; set; }

        public string Id { get; set; }
    }
}
