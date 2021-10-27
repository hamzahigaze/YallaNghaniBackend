using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Models.Account;

namespace YallaNghani.DTOs.Accounts
{
    public class ReadDto
    {
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; } 
        
        public string PhoneNumber { get; set; }
       
        public AccountRole Role { get; set; } 

        public string ImageUrl { get; set; } 

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
