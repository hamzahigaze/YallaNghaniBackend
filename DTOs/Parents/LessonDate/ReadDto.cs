using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Common;

namespace YallaNghani.DTOs.Parents.LessonDate
{
    public class ReadDto
    {
        public Day Day { get; set; }

        public string Hour { get; set; }

        public string Id { get; set; }

    }
}
