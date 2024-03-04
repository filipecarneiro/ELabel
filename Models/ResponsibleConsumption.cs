using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    // Responsible consumption (pictogram warning against drinking during pregnanc and a generic message are Mandatory)

    [Owned]
    public class ResponsibleConsumption
    {
        [Display(Name = "Warning against drinking during pregnanc", Description = "Don't drink during pregnancy and breastfeeding")]
        public bool WarningDrinkingDuringPregnancy { get; set; } = false;

        [Display(Name = "Warning against drinking below legal age", Description = "Don't drink when below legal drinking age")]
        public bool WarningDrinkingBelowLegalAge { get; set; } = false;

        [Display(Name = "Warning against drinking when driving", Description = "Don't drink when driving a car, motorbike or operating machinery")]
        public bool WarningDrinkingWhenDriving { get; set; } = false;

        public bool HasAny()
        {
            if (WarningDrinkingDuringPregnancy || WarningDrinkingBelowLegalAge || WarningDrinkingWhenDriving)
                return true;

            return false;
        }

        public ResponsibleConsumption DeepCopy()
        {
            return (ResponsibleConsumption)this.MemberwiseClone();
        }
    }
}
