using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    /// <summary>
    /// Ingredient category presentation format options
    /// </summary>
    /// <remarks>
    /// (...)categories listed in this Part must be designated by the name of that category, followed by their specific name or, if appropriate, E number.
    /// </remarks>
    /// <see cref="https://eur-lex.europa.eu/legal-content/EN/TXT/HTML/?uri=CELEX:02011R1169-20180101#tocId396"/>
    public enum IngredientListForCategoryFormat
    {
        [Display(Name = "Parentheses", Description = "category (ingredient1, ingredient2)")]
        Parentheses = 0,

        [Display(Name = "Colon", Description = "category: ingredient1, ingredient2")]
        Colon = 1,

        [Display(Name = "E number", Description = "category e-number1 e-number2")]
        ENumber = 2
    }
}
