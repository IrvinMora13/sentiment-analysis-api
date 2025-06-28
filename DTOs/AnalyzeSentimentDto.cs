using System.ComponentModel.DataAnnotations;

namespace SentimentApi.DTOs
{
    public class AnalyzeSentimentDto
    {
        [Required(ErrorMessage = "Text is required")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 1000 characters")]
        public string Text { get; set; } = string.Empty;
    }
}