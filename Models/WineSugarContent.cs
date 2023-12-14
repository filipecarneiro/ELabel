using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    /*
     *  EU Regulation (EU) No 251/2014
     *  Article 6 (Additional particulars to the sales denominations)
     *  https://eur-lex.europa.eu/legal-content/EN/TXT/HTML/?uri=CELEX:02014R0251-20231208#tocId10
     */
    public enum WineSugarContent
    {
        [Display(Name = "extra-dry", Description = "With a sugar content of less than 30 grams per litre and a minimum total alcoholic strength by volume of 15 % vol.")]
        ExtraDry = 0,

        [Display(Name = "dry", Description = "With a sugar content of less than 50 grams per litre and a minimum total alcoholic strength by volume of 16 % vol.")]
        Dry = 1,

        [Display(Name = "semi-dry", Description = "With a sugar content of between 50 and less than 90 grams per litre.")]
        SemiDry = 2,

        [Display(Name = "semi-sweet", Description = "With a sugar content of between 90 and less than 130 grams per litre.")]
        SemiSweet = 3,

        [Display(Name = "sweet", Description = "With a sugar content of 130 grams per litre or more.")]
        Sweet = 4
    }
}
