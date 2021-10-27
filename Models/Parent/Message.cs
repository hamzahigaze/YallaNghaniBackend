using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Parent
{
    public class Message : Entity
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
