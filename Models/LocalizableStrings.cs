using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELabel.Models
{
    [Owned]
    [Display(Name = "Localization", Description = "Localizable strings")]
    public class LocalizableStrings
    {
        [Column("bg")]
        [JsonPropertyName("bg")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Bulgarian", Description = "bg")]
        //[IsoLanguageCode("bg")]
        public string? Bulgarian { get; set; }

        [Column("hr")]
        [JsonPropertyName("hr")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Croatian", Description = "hr")]
        public string? Croatian { get; set; }

        [Column("cs")]
        [JsonPropertyName("cs")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Czech", Description = "cs")]
        public string? Czech { get; set; }

        [Column("da")]
        [JsonPropertyName("da")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Danish", Description = "da")]
        public string? Danish { get; set; }

        [Column("nl")]
        [JsonPropertyName("nl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Dutch", Description = "nl")]
        public string? Dutch { get; set; }

        [Column("en")]
        [JsonPropertyName("en")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "English", Description = "en")]
        public string? English { get; set; }

        [Column("et")]
        [JsonPropertyName("et")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Estonian", Description = "et")]
        public string? Estonian { get; set; }

        [Column("fi")]
        [JsonPropertyName("fi")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Finnish", Description = "fi")]
        public string? Finnish { get; set; }

        [Column("fr")]
        [JsonPropertyName("fr")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "French", Description = "fr")]
        public string? French { get; set; }

        [Column("de")]
        [JsonPropertyName("de")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "German", Description = "de")]
        public string? German { get; set; }

        [Column("el")]
        [JsonPropertyName("el")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Greek", Description = "el")]
        public string? Greek { get; set; }

        [Column("hu")]
        [JsonPropertyName("hu")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Hungarian", Description = "hu")]
        public string? Hungarian { get; set; }

        [Column("ga")]
        [JsonPropertyName("ga")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Irish", Description = "ga")]
        public string? Irish { get; set; }

        [Column("it")]
        [JsonPropertyName("it")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Italian", Description = "it")]
        public string? Italian { get; set; }

        [Column("lv")]
        [JsonPropertyName("lv")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Latvian", Description = "lv")]
        public string? Latvian { get; set; }

        [Column("lt")]
        [JsonPropertyName("lt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Lithuanian", Description = "lt")]
        public string? Lithuanian { get; set; }

        [Column("mt")]
        [JsonPropertyName("mt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Maltese", Description = "mt")]
        public string? Maltese { get; set; }

        [Column("pl")]
        [JsonPropertyName("pl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Polish", Description = "pl")]
        public string? Polish { get; set; }

        [Column("pt")]
        [JsonPropertyName("pt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Portuguese", Description = "pt")]
        public string? Portuguese { get; set; }

        [Column("ro")]
        [JsonPropertyName("ro")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Romanian", Description = "ro")]
        public string? Romanian { get; set; }

        [Column("sk")]
        [JsonPropertyName("sk")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Slovak", Description = "sk")]
        public string? Slovak { get; set; }

        [Column("sl")]
        [JsonPropertyName("sl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Slovene", Description = "sl")]
        public string? Slovene { get; set; }

        [Column("es")]
        [JsonPropertyName("es")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Spanish", Description = "es")]
        public string? Spanish { get; set; }

        [Column("sv")]
        [JsonPropertyName("sv")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Display(Name = "Swedish", Description = "sv")]
        public string? Swedish { get; set; }

        public string? GetLocalized(string isoLanguageCode)
        {
            // TODO: Use property Display description ou a custom atrribute, like IsoLanguageCode
            switch (isoLanguageCode)
            {
                case "bg":
                    return this.Bulgarian;

                case "hr":
                    return this.Croatian;

                case "cs":
                    return this.Czech;

                case "da":
                    return this.Danish;

                case "nl":
                    return this.Dutch;

                case "en":
                    return this.English;

                case "et":
                    return this.Estonian;

                case "fi":
                    return this.Finnish;

                case "fr":
                    return this.French;

                case "de":
                    return this.German;

                case "el":
                    return this.Greek;

                case "hu":
                    return this.Hungarian;

                case "ga":
                    return this.Irish;

                case "it":
                    return this.Italian;

                case "lv":
                    return this.Latvian;

                case "lt":
                    return this.Lithuanian;

                case "mt":
                    return this.Maltese;

                case "pl":
                    return this.Polish;

                case "pt":
                    return this.Portuguese;

                case "ro":
                    return this.Romanian;

                case "sk":
                    return this.Slovak;

                case "sl":
                    return this.Slovene;

                case "es":
                    return this.Spanish;

                case "sv":
                    return this.Swedish;
            }

            return null;
        }
    }
}
