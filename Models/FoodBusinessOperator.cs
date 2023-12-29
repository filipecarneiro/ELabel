using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELabel.Models
{
    [Owned]
    [Display(Name = "Food business operator", Description = "Operator under whose name or business name the food is marketed or, if that operator is not established in the Union, the importer into the Union market.")]
    public class FoodBusinessOperator
    {
        [Column("FBOName")]
        [Ganss.Excel.Column("FBOName")]
        [Display(Name = "Name", Description = "Food business operator name, to be displayed if business name or address not defined.")] 
        [StringLength(100)]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("FBOBusinessName")]
        [Ganss.Excel.Column("FBOBusinessName")]
        [Display(Name = "Business name", Description = "Food business operator business name, to be displayed with the address.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string? BusinessName { get; set; }

        [Column("FBOAddress")]
        [Ganss.Excel.Column("FBOAddress")]
        [Display(Name = "Address", Description = "Food business operator address, to be displayed with the business name.")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string? Address { get; set; }

        public FoodBusinessOperator DeepCopy()
        {
            return (FoodBusinessOperator)this.MemberwiseClone();
        }

        public string? ToText()
        {
            if (string.IsNullOrWhiteSpace(BusinessName) || string.IsNullOrWhiteSpace(Address))
                return Name;

            return $"{BusinessName} - {Address}";
        }
    }
}
