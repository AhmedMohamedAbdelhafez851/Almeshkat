using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class TeacherSubjectDto
    {
        public int Id { get; set; } 
        public int StaSubjId { get; set; }

        public int TeacherId { get; set; }

        public string? TeacherName { get; set; } 

        public string? SubjectName { get; set; }

        public string? CreatedBy { get; set; }
       
    }

}
