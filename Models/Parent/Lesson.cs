using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using YallaNghani.Models.Core;

namespace YallaNghani.Models.Parent
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "paied")]
        Paied, 
        
        [EnumMember(Value="unpaied")]
        UnPaied
    }
    public class Lesson : Entity
    {
        [Required]
        public int? Number { get; set; } = null;

        [Required]
        public DateTime? Date { get; set; } = null;

        [Required]
        public PaymentStatus? PaymentStatus { get; set; } = null;

        public string? PaymentNote { get; set; } = null;

        [Required]
        public double? Fee { get; set; } = null;

        public string? Summary { get; set; } = null;
    }
}
