using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    /*
     *  EU Commission Regulation (EC) No 2019/33
     *  Article 48a 6. (List of ingredients)
     *  https://eur-lex.europa.eu/legal-content/EN/TXT/HTML/?uri=CELEX:02019R0033-20231208
     */
    public enum PackagingGases
    {
        [Display(Name = "(none)")]
        None = 0,

        [Display(Name = "Bottling may happen in a protective atmosphere")]
        BottlingMayHappenInAProtectiveAtmosphere = 1,

        [Display(Name = "Bottled in a protective atmosphere")]
        BottledInAProtectiveAtmosphere = 2
    }
}
