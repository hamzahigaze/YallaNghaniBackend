using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Parents.Course
{
    public class ChangeTeacherDto
    {
        [Required]
        public String? newTeacherId { get; set; } = null;
    }
}
