using ELabel.Models;

namespace ELabel.ViewModels
{
    public class IngredientExcelDto
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required IngredientCategory Category { get; set; }

        public ushort? ENumber { get; set; }

        public required bool Allergen { get; set; }

        public required bool Custom { get; set; }

        public required LocalizableStrings LocalizableStrings { get; set; } = new();
    }
}
