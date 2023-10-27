using System;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        public required DateTime CreatedOn { get; set; }

        [MaxLength(256)]
        public required string CreatedBy { get; set; }

        public required DateTime UpdatedOn { get; set; }

        [MaxLength(256)]
        public required string UpdatedBy { get; set;}
    }
}
