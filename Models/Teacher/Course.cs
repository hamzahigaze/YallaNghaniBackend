using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Common;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Teacher
{
    public class Course : Entity
    {       

        [Required]
        public string? StudentName { get; set; } = null;

        [Required]
        public string? Title { get; set; } = null;

        [Required]
        public string? ParentId { get; set; } = null;

        [Required]
        public List<LessonDate> LessonsDates { get; set; } = new List<LessonDate>();
    }
}
