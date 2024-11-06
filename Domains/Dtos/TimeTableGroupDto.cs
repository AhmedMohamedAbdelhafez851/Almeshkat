using Domains.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class TimeTableGroupDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(ErrorMessage = ErrorMessages.MaxLength)]
        public string TimTabGroName { get; set; } = "";

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int TermId { get; set; }  

        public string? TermName { get; set; }  // this property used only for retrive 

    }
}
