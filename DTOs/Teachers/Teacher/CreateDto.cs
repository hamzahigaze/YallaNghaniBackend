using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeacherEntity = YallaNghani.Models.Teacher;

namespace YallaNghani.DTOs.Teachers.Teacher
{
    public class CreateDto
    {
        [Required]
        public string? PhoneNumber { get; set; } = null;

        [Required]
        public string? FirstName { get; set; } = null;

        [Required]
        public string? LastName { get; set; } = null;
    }
}
