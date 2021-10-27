using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Accounts
{
    public class UpdateProfileDto
    {
        
        public string? FirstName { get; set; } = null;
        
        public string? LastName { get; set; } = null;

        [Phone]
        public string? PhoneNumber { get; set; } = null;


    }
}
