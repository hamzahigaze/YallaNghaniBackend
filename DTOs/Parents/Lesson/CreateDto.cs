using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ParentEntity = YallaNghani.Models.Parent;


namespace YallaNghani.DTOs.Parents.Lesson
{
    public class CreateDto
    {
        [Required]
        public DateTime? Date { get; set; } = null;

        public string? Summary { get; set; } = null;

        public string? PaymentNote { get; set; } = null;

    }
}
