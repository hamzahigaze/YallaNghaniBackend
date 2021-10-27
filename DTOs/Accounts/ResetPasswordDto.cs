using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Accounts
{
    public class ResetPasswordDto
    {
        [Required]
        [MinLength(8)]
        public string? Password { get; set; } = null;

        [Required]
        public string? AccountId { get; set; } = null;
    }
}
