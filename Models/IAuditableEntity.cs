using System;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    public interface IAuditableEntity
    {
        [Display(Name = "Created on")]
        DateTime CreatedOn { get; set; }

        [Display(Name = "Created by")]
        string CreatedBy { get; set; }

        [Display(Name = "Updated on")]
        DateTime UpdatedOn { get; set; }

        [Display(Name = "Updated by")]
        string UpdatedBy { get; set; }

        DateTime CreatedOnToLocalTime
        {
            get
            {
                return CreatedOn.ToLocalTime();
            }
        }

        DateTime UpdatedOnToLocalTime { 
            get {
                return UpdatedOn.ToLocalTime();
            }
        }
    }
}
