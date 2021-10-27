using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Parents.Message
{
    public class CreateDto
    {
        [Required]
        public string? Title { get; set; } = null;

        [Required]
        public string? Content { get; set; } = null;

        [Required]
        public string? SenderName { get; set; } = null;

        [Required]
        public DateTime? Date { get; set; } = null;
    }
}
