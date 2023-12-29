using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;

namespace ELabel.TagHelpers
{
    /*
     * Display of nutrient values with tolerance and rounding defined by EU Regulation No 1169/2011
     * https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02011R1169-20180101&qid=1701362311604
     * See the Commission services summary table here:
     * https://food.ec.europa.eu/system/files/2016-10/labelling_nutrition-vitamins_minerals-guidance_tolerances_summary_table_012013_en.pdf
     */

    public enum ValueRange
    {
        Zero,
        Low,    // Tolerable
        Normal,
        High
    }

    public class NutrientTagHelper : TagHelper
    {
        [HtmlAttributeName("value")]
        public float Value { get; set; }

        [HtmlAttributeName("tolerance")]
        public float Tolerance { get; set; } = 0.5f;

        [HtmlAttributeName("max-decimals")]
        public short MaxDecimals { get; set; } = 1;

        [HtmlAttributeName("unit")]
        public string Unit { get; set; } = $"g";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            // High value infered from EU Tolerance summary table
            float highValue = float.MaxValue;

            if (MaxDecimals == 1)
                highValue = 10;
            else if (MaxDecimals >= 2)
                highValue = 1;

            ValueRange valueRange;
            short decimals;

            if (Value <= 0)
            {
                valueRange = ValueRange.Zero;
                decimals = 0;
            }
            else if(Value > 0 && Value <= Tolerance)
            { 
                valueRange = ValueRange.Low;
                decimals = MaxDecimals;
            }
            else if(Value > Tolerance && Value < highValue) 
            {
                valueRange = ValueRange.Normal;
                decimals = MaxDecimals;
            }
            else // Value >= highValue
            {
                valueRange = ValueRange.High;
                decimals = (short)(MaxDecimals > 0 ? MaxDecimals - 1 : 0);
            }

            CultureInfo currentCultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            NumberFormatInfo numberFormatInfo = currentCultureInfo.NumberFormat;

            numberFormatInfo.NumberDecimalDigits = decimals;

            string valueText;
            switch (valueRange)
            {
                case ValueRange.Zero:
                    valueText = "0";
                    break;
                case ValueRange.Low:
                    valueText = "< " + Tolerance.ToString("N", numberFormatInfo);
                    break;
                default:
                case ValueRange.Normal:
                case ValueRange.High:
                    valueText = Value.ToString("N", numberFormatInfo);
                    break;
            }

            string nutrientHtml = $"<span class='display' data-value='{Value}' data-value-range='{valueRange.ToString().ToLower()}'>{HttpUtility.HtmlEncode(valueText)}</span>&nbsp;<span class='unit'>{Unit}</span>";

            output.Attributes.SetAttribute("class", "nutrient");
            output.Content.SetHtmlContent(nutrientHtml);
        }
    }
}
