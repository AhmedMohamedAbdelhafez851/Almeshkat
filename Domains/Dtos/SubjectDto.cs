using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(ErrorMessage = ErrorMessages.MaxLength)]
        public string SubjectName { get; set; } = "";
    }
}
