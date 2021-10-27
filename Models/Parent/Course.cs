using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Common;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Parent
{
    public class Course : Entity
    {
        [Required()]
        public string? TeacherId { get; set; } = null;
        [Required()]
        public string? Title { get; set; } = null;
        [Required()]
        public string? StudentName { get; set; } = null;
        [Required()]
        public string? TeacherName { get; set; } = null;
        [Required()]
        public double? LessonFee { get; set; } = null;
        [Required()]
        public double? ParentBalance { get; set; } = 0.0;
        [Required()]
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        [Required()]
        public List<Payment> Payments { get; set; } = new List<Payment>();
        [Required()]
        public List<LessonDate> LessonsDates{ get; set; } = new List<LessonDate>();
    }
}
