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

        public string? Country { get; set; }

        public string? Sku { get; set; }

        [Ganss.Excel.DataFormat("0")]
        public ulong? Ean { get; set; }

        // Export only properties

        public string Code { get; set; } = String.Empty;

        [Ganss.Excel.Formula]
        public string? ShortUrl { get; set; }
        [Ganss.Excel.Formula]
        public string? QRCode { get; set; }
        [Ganss.Excel.Formula]
        public string? QRCodePNG { get; set; }
    }
}
