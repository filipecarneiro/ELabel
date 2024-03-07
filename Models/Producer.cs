namespace ELabel.Models
{
    public class Producer
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Website { get; set; }
        public IngredientListForCategoryFormat IngredientListFormat { get; set; } = IngredientListForCategoryFormat.Parentheses;
    }
}
