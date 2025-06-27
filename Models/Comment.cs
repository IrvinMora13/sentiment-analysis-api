using System.ComponentModel.DataAnnotations;

namespace SentimentsApi.Models{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(1000)]
        public string CommentText { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Sentiment { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}