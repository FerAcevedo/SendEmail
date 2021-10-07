using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class EmailDto
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid e-mail address")]
        public string EmailAddress { get; set; }
        
        public string RefNumber { get; set; }

        public string SLA { get; set; }

        public string TranslationText { get; set; }    
    }
}