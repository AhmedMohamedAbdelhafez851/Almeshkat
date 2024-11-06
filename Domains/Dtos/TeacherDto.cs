using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public string UserId { get; set; } = "";

        [Url(ErrorMessage = ErrorMessages.InvalidUrl)]
        public string ZoomLink { get; set; } = "";
    }
}

