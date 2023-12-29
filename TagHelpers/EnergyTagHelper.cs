using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;

namespace ELabel.TagHelpers
{
     /*
     * Display of energy values with tolerance and rounding defined by EU Regulation No 1169/2011
     * https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02011R1169-20180101&qid=1701362311604
     * See the Commission services summary table here:
     * https://food.ec.europa.eu/system/files/2016-10/labelling_nutrition-vitamins_minerals-guidance_tolerances_summary_table_012013_en.pdf
     */

    public class EnergyTagHelper : TagHelper
    {
        [HtmlAttributeName("value")]
        public float Value { get; set; }

        [HtmlAttributeName("unit")]
        public string Unit { get; set; } = $"kJ";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            // Allways zero decimal digits. Rounding to the nearest 1 kJ/kcal
            string valueText = Value.ToString("N0");
            
            string energyHtml = $"<span class='display' data-value='{Value}'>{HttpUtility.HtmlEncode(valueText)}</span>&nbsp;<span class='unit'>{Unit}</span>";
            
            output.Attributes.SetAttribute("class", "energy");
            output.Content.SetHtmlContent(energyHtml);
        }
    }
}
