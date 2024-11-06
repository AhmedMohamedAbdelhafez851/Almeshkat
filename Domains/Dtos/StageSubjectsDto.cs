using System;

namespace Domains.Dtos
{
    public class StageSubjectsDto
    {
        public int StageSubjectId { get; set; }
        public int TermId { get; set; }
        public int SubStageId { get; set; }
        public int SubjectId { get; set; }
        public string? TermName { get; set; }
        public string? SubStageName { get; set; }
        public string? SubjectName { get; set; }
    }
}
