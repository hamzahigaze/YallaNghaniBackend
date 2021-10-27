using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Common;

namespace YallaNghani.DTOs.Parents.LessonDate
{
    public class CreateDto
    {
        [Required]
        public Day? Day { get; set; } = null;
        
        [Required]
        public string? Hour { get; set; } = null;

    }
}
