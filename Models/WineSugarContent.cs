using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    /*
     *  Wine sugar content
     *  
     *  EU Commission Delegated Regulation (EU) 2019/33 for **Sparkling wine** (Part A) and **Others** (Part B)
     *  ANNEX III
     *  https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX:02019R0033-20231208#anx_III
     * 
     *  Sparkling wines: Brut nature, Extra brut, Brut, Extra dry, Dry, Medium dry or Sweet. (7)
     *  Other wines: Dry, Medium dry, Medium sweet or Sweet (4)
     */
    public enum WineSugarContent
    {
        [Display(Name = "Brut nature", Description = "For sparkling wine.")]
        BrutNature = -4,

        [Display(Name = "Extra brut", Description = "For sparkling wine.")]
        ExtraBrut = -3,

        [Display(Name = "Brut", Description = "For sparkling wine.")]
        Brut = -2,

        [Display(Name = "Extra dry", Description = "For sparkling wine and aromatised wine.")]
        ExtraDry = -1,

        [Display(Name = "Dry", Description = "For sparkling wine and all others.")]
        Dry = 1,

        [Display(Name = "Medium dry", Description = "For sparkling wine and all others.")]
        MediumDry = 2,

        [Display(Name = "Medium sweet", Description = "For all other wines.")]
        MediumSweet = 3,

        [Display(Name = "Sweet", Description = "For sparkling wine and all others.")]
        Sweet = 4
    }
}
