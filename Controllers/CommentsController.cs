using Microsoft.AspNetCore.Mvc;
using SentimentApi.Data;
using SentimentApi.Models;
using SentimentApi.Services;
using SentimentApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SentimentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsContext _context;
        private readonly InterfaceSentimentAnalyzer _analyzer;
        private readonly ILogger<CommentsController> _logger;

        private readonly string[] _validSentiments = { "positivo", "negativo", "neutral" };
        public CommentsController(CommentsContext context, InterfaceSentimentAnalyzer analyzer,ILogger<CommentsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _analyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }

        // POST /api/comments
        [HttpPost]
        public async Task<ActionResult<CommentResponseDto>> PostComment([FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido en PostComment: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var sentiment = _analyzer.AnalyzeSentiment(dto.Text);

            var comment = new Comment
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                CommentText = dto.Text,
                Sentiment = sentiment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var response = new CommentResponseDto
            {
                Id = comment.Id,
                ProductId = comment.ProductId,
                UserId = comment.UserId,
                CommentText = comment.CommentText,
                Sentiment = comment.Sentiment,
                CreatedAt = comment.CreatedAt
            };

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, response);
        }

        // GET /api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetComments([FromQuery] string? product_id, [FromQuery] string? sentiment)
        {
            var query = _context.Comments.AsQueryable();

            if (!string.IsNullOrEmpty(product_id))
                query = query.Where(c => c.ProductId == product_id);

            if (!string.IsNullOrEmpty(sentiment))
                query = query.Where(c => c.Sentiment == sentiment);

            var comments = await query
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    UserId = c.UserId,
                    CommentText = c.CommentText,
                    Sentiment = c.Sentiment,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            return Ok(comments);
        }

        // GET /api/comments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponseDto>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                _logger.LogInformation("Comentario no encontrado: {Id}", id);
                return NotFound();
            }

            var response = new CommentResponseDto
            {
                Id = comment.Id,
                ProductId = comment.ProductId,
                UserId = comment.UserId,
                CommentText = comment.CommentText,
                Sentiment = comment.Sentiment,
                CreatedAt = comment.CreatedAt
            };

            return Ok(response);
        }

        // GET /api/sentiment-summary
        [HttpGet("/api/sentiment-summary")]
        public async Task<ActionResult<object>> GetSentimentSummary()
        {
            var total = await _context.Comments.CountAsync();
            var counts = await _context.Comments
                .GroupBy(c => c.Sentiment)
                .Select(g => new { Sentiment = g.Key, Count = g.Count() })
                .ToListAsync();

            var summary = new
            {
                total_comments = total,
                sentiment_counts = counts.ToDictionary(x => x.Sentiment, x => x.Count)
            };

            return Ok(summary);
        }
    }
}