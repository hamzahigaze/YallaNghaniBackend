using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Common;

namespace YallaNghani.DTOs.Teachers.Course
{
    public class CreateDto
    {
        [Required]
        public string? Title { get; set; } = null;

        [Required]
        public string? StudentName { get; set; } = null;

        [Required]
        public string? ParentId { get; set; } = null;

        public List<LessonDate> LessonsDates { get; set; } = new List<LessonDate>();

    }
}
