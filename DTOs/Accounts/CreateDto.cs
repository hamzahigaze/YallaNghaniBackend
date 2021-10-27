using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Account;

namespace YallaNghani.DTOs.Accounts
{
    public class CreateDto
    {
        [Required]
        public string? UserName { get; set; } = null;

        [Required]
        public string? FirstName { get; set; } = null;

        [Required]
        public string? LastName { get; set; } = null;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = null;

        [Required]
        public AccountRole? Role { get; set; } = null;
    }
}
