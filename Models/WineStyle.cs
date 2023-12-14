using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    /*
     *  EU Commission Regulation (EC) No 753/2002
     *  Article 16 (Indication of product type)
     *  https://eur-lex.europa.eu/legal-content/EN/TXT/PDF/?uri=CELEX:32002R0753
     */
    [Obsolete("Use WineSugarContent")]
    public enum WineStyle
    {
        [Display(Name = "Dry")]
        Dry = 1,

        [Display(Name = "Medium dry")]
        MediumDry = 2,

        [Display(Name = "Medium sweet")]
        MediumSweet = 3,

        [Display(Name = "Sweet")]
        Sweet = 4
    }
}
