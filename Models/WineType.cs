using System.ComponentModel.DataAnnotations;

namespace ELabel.Models
{
    /*
     *  ISWN Wine Style Framework (WSF)
     *  https://web.archive.org/web/20110809074649/http://www.iswn.org/iswn_org/download/Introduction_to_ISWN_download.pdf
     *  https://web.archive.org/web/20110809075618/http://www.iswn.org/iswn_org/download/ISWN%20Wine%20Style%20Framework%20V1.1.pdf
     */

    public enum WineType
    {
        [Display(Name = "White", Description = "White wine")]
        White = 1,

        [Display(Name = "Red", Description = "Red wine")]
        Red = 2,

        [Display(Name = "Rosé", Description = "Rosé wine")]
        Rose = 3,

        [Display(Name = "Sparkling", Description = "Sparkling wine")]
        Sparkling = 4,

        [Display(Name = "Fortified", Description = "Fortified wine")]
        Fortified = 5,

        [Display(Name = "Orange", Description = "Orange wine")]
        Orange = 6
    }
}
