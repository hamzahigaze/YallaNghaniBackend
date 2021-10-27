using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommonModels = YallaNghani.Models.Common;
using YallaNghani.Models.Parent;
using YallaNghani.DTOs.Parents.LessonDate;

namespace YallaNghani.DTOs.Parents.Course
{
    public class CreateDto
    {

        [Required]
        public string? TeacherId { get; set; } = null;

        [Required]
        public string? Title { get; set; } = null;

        [Required]
        public string? StudentName { get; set; } = null;

        [Required]
        public double? LessonFee { get; set; } = null;

    }
}
