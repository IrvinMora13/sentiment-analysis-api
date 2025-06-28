using System.ComponentModel.DataAnnotations;

namespace SentimentApi.DTOs
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "Product ID is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Product ID must be between 1 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\-_]+$", ErrorMessage = "Product ID can only contain letters, numbers, hyphens, and underscores")]
        public string ProductId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Comment text is required")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Comment must be between 5 and 1000 characters")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "User ID is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "User ID must be between 1 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\-_@.]+$", ErrorMessage = "User ID contains invalid characters")]
        public string UserId { get; set; } = string.Empty;

        [RegularExpression("^(positivo|negativo|neutral)$", ErrorMessage = "Sentiment must be 'positivo', 'negativo', or 'neutral'")]
        public string? Sentiment { get; set; }
    }
}