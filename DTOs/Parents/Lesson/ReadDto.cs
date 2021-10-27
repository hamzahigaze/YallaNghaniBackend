using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParentEntity = YallaNghani.Models.Parent;
namespace YallaNghani.DTOs.Parents.Lesson
{
    public class ReadDto
    {
        
        public int Number { get; set; } 
        
        public DateTime Date { get; set; } 
        
        public ParentEntity.PaymentStatus PaymentStatus { get; set; } 

        public string PaymentNote { get; set; } 
        
        public double Fee { get; set; } 

        public string Summary { get; set; }

        public string Id { get; set; }
    }
}
