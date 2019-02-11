using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmsDemo.Web.Models
{
    public class IndexViewModel
    {
        [Required]
        [MaxLength(15)]
        public string Message { get; set; }

        [Required]
        [MaxLength(200)]
        public string Mobile { get; set; }
    }
}