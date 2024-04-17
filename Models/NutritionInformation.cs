using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    public class LabAnalysis
    {
        [Display(Name = "Alcohol", Description = "Alcohol as measured by lab (% vol.)")]
        [DisplayFormat(DataFormatString = "{0:G} % vol.")]
        public float Alcohol { get; set; } = 0f;

        [Display(Name = "Residual sugar", Description = "Glucose + Fructose (g/dm3)")]
        [DisplayFormat(DataFormatString = "{0:G} g/dm3")]
        public float ResidualSugar { get; set; } = 0f;

        [Display(Name = "Total acidity", Description = "Total acidity (g/dm3 tartaric)")]
        [DisplayFormat(DataFormatString = "{0:G} g/dm3 (tartaric)")]
        public float TotalAcidity { get; set; } = 0f;

        public float Kilocalories
        {
            get
            {
                return ((7.9f * Alcohol) * 7) + (ResidualSugar * 4) + (TotalAcidity * 4);
            }
        }

        public float GetKilocaloriesInVolume(float millilitres)
        {
            return (millilitres / 1000) * Kilocalories;
        }
    }

    [Owned]
    public class Energy
    {
        public Energy()
        {
            Kilocalorie = 0;
            Kilojoule = null;
        }

        public Energy(float kilocalorie)
        {
            Kilocalorie = kilocalorie;
            Kilojoule = null;
        }

        public Energy(float kilocalorie, float? kilojoule)
        {
            Kilocalorie = kilocalorie;
            Kilojoule = kilojoule;
        }

        [Display(Name = "Kilocalories", Description = "Energy (kcal)")]
        [DisplayFormat(DataFormatString = "{0:G} kcal")]
        public float Kilocalorie { get; set; }

        [Display(Name = "Kilojoules", Description = "Energy (kJ)")]
        [DisplayFormat(DataFormatString = "{0:G} kJ")]
        public float? Kilojoule { get; set; }


        [Display(Name = "Kilojoules", Description = "Energy (kJ)")]
        [DisplayFormat(DataFormatString = "{0:G} kJ")]
        [Ganss.Excel.Ignore]
        public float KilojouleValue
        {
            get
            {
                if (Kilojoule != null)
                    return Kilojoule.Value;

                return MathF.Round(4.184f * Kilocalorie, 1);
            }
        }
    }

    [Owned]
    public class NutritionInformation
    {
        [Display(Name = "Portion", Description = "Volume of a portion (ml)")]
        [DisplayFormat(DataFormatString = "{0:G} ml")]
        public float PortionVolume { get; set; } = 100f;

        [Display(Name = "Energy", Description = "Energy (kcal/kJ)")]
        public Energy Energy { get; set; } = new();

        [Display(Name = "Fat", Description = "Fat (g)")]
        [DisplayFormat(DataFormatString = "{0:G} g")]
        public float FatTotal { get; set; } = 0;

        [Display(Name = "Saturates", Description = "Saturated fat (g)")]
        [DisplayFormat(DataFormatString = "{0:G} g")]
        public float FatSaturates { get; set; } = 0;

        [Display(Name = "Carbohydrate", Description = "Carbohydrate (g)")]
        [DisplayFormat(DataFormatString = "{0:G} g")]
        public float CarbohydrateTotal { get; set; } = 0;

        [Display(Name = "Sugar", Description = "Sugar carbohydrate (g)")]
        [DisplayFormat(DataFormatString = "{0:G} g")]
        public float CarbohydrateSugar { get; set; } = 0;

        [Display(Name = "Protein", Description = "Protein (g)")]
        [DisplayFormat(DataFormatString = "{0:G} g")]
        public float Protein { get; set; } = 0;

        [Display(Name = "Salt", Description = "Salt (g)")]
        [DisplayFormat(DataFormatString = "{0:G} g")]
        public float Salt { get; set; } = 0;

        public NutritionInformation DeepCopy()
        {
            NutritionInformation other = (NutritionInformation)this.MemberwiseClone();
            
            other.Energy = new Energy(this.Energy.Kilocalorie, this.Energy.Kilojoule);

            return other;
        }
    }
}
