using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Parents.Course
{
    public class UpdateDto
    {
        public string? Title { get; set; } = null;

        public string? StudentName { get; set; } = null;

        public double? LessonFee { get; set; } = null;

    }
}
