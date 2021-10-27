using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Parents.Payment
{
    public class CreateDto
    {
        [Required]
        public double? Amount { get; set; } = null;

        [Required]
        public DateTime? Date { get; set; } = null;

        [Required]
        public string? PaymentMethod { get; set; } = null;
    }
}
