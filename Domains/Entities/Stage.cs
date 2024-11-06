// Stage.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Stage : AuditableEntity
    {
        public int StageId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        public string StageName { get; set; } = "";

        public ICollection<SubStage> SubStages { get; set; } = new List<SubStage>();
    }
}
