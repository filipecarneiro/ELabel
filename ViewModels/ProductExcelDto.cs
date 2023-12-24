using ELabel.Models;
using Ganss.Excel;

namespace ELabel.ViewModels
{

    public class ProductExcelDto : AuditableEntity
    {
        public required string Name { get; set; }

        public float? Volume { get; set; }

        [Ganss.Excel.Column("Image")]
        public string? ImageDataUrl { get; set; }

        public required WineInformation WineInformation { get; set; } = new();

        [Ganss.Excel.Json]
        [Ganss.Excel.Column("Ingredients")]
        public List<Guid>? IngredientIdList { get; set; }

        public required PackagingGases PackagingGases { get; set; }

        public required NutritionInformation NutritionInformation { get; set; } = new();

        public required ResponsibleConsumption ResponsibleConsumption { get; set; } = new();

        public required Certifications Certifications { get; set; } = new();

        public required FoodBusinessOperator FoodBusinessOperator { get; set; } = new();

        public required Logistics Logistics { get; set; } = new();

        // Export only properties

        public string? Code { get; set; }

        public string? ShortUrl { get; set; }
    }
}
