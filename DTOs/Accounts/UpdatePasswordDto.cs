using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Accounts
{
    public class UpdatePasswordDto
    {
        [Required]
        public string? OldPassword { get; set; } = null;

        [Required]
        public string? NewPassword { get; set; } = null;
    }
}
