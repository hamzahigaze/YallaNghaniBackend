using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string? UserName { get; set; } = null;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string? Password { get; set; } = null;
    }
}
