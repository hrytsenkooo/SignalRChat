using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Sentiment { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
