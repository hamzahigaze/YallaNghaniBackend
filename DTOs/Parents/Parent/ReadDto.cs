using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParentEntity = YallaNghani.Models.Parent;

namespace YallaNghani.DTOs.Parents.Parent
{
    public class ReadDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Id { get; set; }

        public IEnumerable<ParentEntity.Course> Courses { get; set; }

        public IEnumerable<ParentEntity.Message> Messages { get; set; }

        public int NewMessagesCount { get; set; }

        public bool HasLessonsDatesUpdate { get; set; }
    }
}
