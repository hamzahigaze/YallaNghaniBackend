using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Common
{
    public class LessonDate : Entity
    {
        [Required]
        public Day? Day { get; set; } = null;

        [Required]
        public string? Hour { get; set; } = null;
    }
}
