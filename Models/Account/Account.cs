using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Account
{
    public enum AccountRole
    {
        [EnumMember]
        Parent,

        [EnumMember]
        Teacher,

        [EnumMember]
        Admin
    }
    public class Account : Entity
    {

        [Required]
        public string? UserName { get; set; } = null;

        [Required]
        public string? FirstName { get; set; } = null;

        [Required]
        public string? LastName { get; set; } = null;

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; } = null;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string? Password { get; set; } = null;

        [Required]
        public AccountRole? Role { get; set; } = null;

        public string? ImageUrl { get; set; } = null;
    }
}
