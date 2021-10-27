using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.DTOs.Parents.Payment
{
    public class ReadDto
    {
        
        public double Amount { get; set; } 

        
        public DateTime Date { get; set; } 

        
        public string PaymentMethod { get; set; }

        public string Id { get; set; }
    }
}
