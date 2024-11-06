// Term.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Term : AuditableEntity
    {
        public int TermId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string TermName { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime TermStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime TermEndDate { get; set; }

        public int YearId { get; set; }
        public Year? Year { get; set; }

        public ICollection<StageSubject> StageSubjects { get; set; } = new List<StageSubject>();
        public ICollection<TimeTableGroup> TimeTableGroups { get; set; } = new List<TimeTableGroup>();
    }
}
