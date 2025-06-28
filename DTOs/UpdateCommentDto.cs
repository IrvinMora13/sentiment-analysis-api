using System.ComponentModel.DataAnnotations;

namespace SentimentApi.DTOs
{
    public class UpdateCommentDto
    {
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Comment must be between 5 and 1000 characters")]
        public string? Text { get; set; }

        [RegularExpression("^(positivo|negativo|neutral)$", ErrorMessage = "Sentiment must be 'positivo', 'negativo', or 'neutral'")]
        public string? Sentiment { get; set; }
    }
}