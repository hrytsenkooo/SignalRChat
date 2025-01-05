namespace ChatAPI.Models
{
    public class ChatMessageDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Sentiment { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } 
    }
}
