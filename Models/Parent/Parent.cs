using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Parent
{
    public class Parent : Entity
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public List<Course> Courses { get; set; } = new List<Course>();

        [Required]
        public List<Message> Messages { get; set; } = new List<Message>();

        [Required]
        public int NewMessagesCount { get; set; } = 0;

        [Required]
        public bool HasLessonsDatesUpdate { get; set; } = false;

    }
}
