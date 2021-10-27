using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.OfferedCourse
{
    public class UpdateDto
    {
        public string? Title { get; set; } = null;

        public string? Description { get; set; } = null;

        public string? TeacherName { get; set; } = null;

        public string? TeacherDescription { get; set; } = null;

        public bool? IsEducational { get; set; } = null;

        public string? ImageUrl { get; set; } = null;


    }
}
