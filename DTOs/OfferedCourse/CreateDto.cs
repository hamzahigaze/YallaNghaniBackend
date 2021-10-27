using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.OfferedCourse
{
    public class CreateDto
    {
        [Required]
        public string? Title { get; set; } = null;

        [Required]
        public string? Description { get; set; } = null;

        [Required]
        public string? TeacherName { get; set; } = null;

        [Required]
        public string? TeacherDescription { get; set; } = null;

        [Required]
        public bool? IsEducational { get; set; } = null;

        [Required]
        public string? ImageUrl { get; set; } = null;
    }
}
