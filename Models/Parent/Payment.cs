using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Parent
{
    public class Payment : Entity
    {
        [Required]
        public double? Amount { get; set; } = null;

        [Required]
        public DateTime? Date { get; set; } = null;

        [Required]
        public string? PaymentMethod { get; set; } = null;

    }
}
