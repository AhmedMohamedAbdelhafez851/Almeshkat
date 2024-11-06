using Domains.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class StudentDto
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(ErrorMessage = ErrorMessages.MaxLength)]
        public string UserId { get; set; } = "";
        public string? FullName { get; set; }    

        public string? Email { get; set; }  

        public int? SubStageId { get; set; }

        public string? SubStageName { get; set; }

        



    }
}
