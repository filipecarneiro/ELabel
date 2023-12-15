using System.ComponentModel.DataAnnotations;

// TODO: Review with this (Part C of ANNEX VII) https://eur-lex.europa.eu/legal-content/EN/TXT/HTML/?uri=CELEX:02011R1169-20180101#tocId396
namespace ELabel.Models
{
    public enum IngredientCategory
    {
        [Display(Name = "Other ingredient")]
        Other = 0,

        [Display(Name = "Acidity regulator")]
        AcidityRegulator = 1,

        //[Display(Name = "Allergen")]
        //Allergen = 2,

        [Display(Name = "Antioxidant")]
        Antioxidant = 3,

        [Display(Name = "Clarifying agent")]
        ClarifyingAgent = 4,

        [Display(Name = "Correction of defect")]
        CorrectionOfDefect = 5,

        [Display(Name = "Enrichment substance")]
        EnrichmentSubstance = 6,

        [Display(Name = "Enzyme")]
        Enzyme = 7,

        [Display(Name = "Activators for alcoholic and malolactic fermentation")]
        ActivatorsForAlcoholicAndMalolacticFermentation = 8,

        [Display(Name = "Fermentation agent")]
        FermentationAgent = 9,

        [Display(Name = "Gases and packaging gas")]
        GasesAndPackagingGas = 10,

        [Display(Name = "Preservative")]
        Preservative = 11,

        [Display(Name = "Raw material")]
        RawMaterial = 12,

        [Display(Name = "Sequestrant")]
        Sequestrant = 13,

        [Display(Name = "Stabiliser")]
        Stabiliser = 14,

        [Display(Name = "Sweetener")]
        Sweetener = 15
    }
}
