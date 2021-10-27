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
    public class Teacher : Entity
    {
        [Required]
        public string? AccountId { get; set; } = null;

        [Required]
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
