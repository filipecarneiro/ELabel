using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Owned]
    [Display(Name = "Food business operator", Description = "Operator under whose name or business name the food is marketed or, if that operator is not established in the Union, the importer into the Union market.")]
    public class FoodBusinessOperator
    {
        [Column("FBOType")]
        [Ganss.Excel.Column("FBOType")]
        [Display(Name = "Type", Description = "Indication of the bottler, producer, importer or vendor.")]
        public FoodBusinessOperatorType Type { get; set; }

        [Column("FBOName")]
        [Ganss.Excel.Column("FBOName")]
        [Display(Name = "Name", Description = "Food business operator name.")] 
        [StringLength(100)]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("FBOAddress")]
        [Ganss.Excel.Column("FBOAddress")]
        [Display(Name = "Address", Description = "Food business operator address.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string? Address { get; set; }

        [Column("FBOAdditionalInfo")]
        [Ganss.Excel.Column("FBOAdditionalInfo")]
        [Display(Name = "Additional information", Description = "Optional indications, like a code, VAT number or additional Impressum information.")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [MaxLength(100)]
        public string? AdditionalInfo { get; set; }

        public FoodBusinessOperator DeepCopy()
        {
            return (FoodBusinessOperator)this.MemberwiseClone();
        }
    }
}
