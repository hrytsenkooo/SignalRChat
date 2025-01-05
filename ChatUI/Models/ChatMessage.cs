namespace ChatUI.Models
{
    public class ChatMessage
    {
        public string UserName { get; set; } = "";
        public string Text { get; set; } = "";
        public bool IsCurrentUser { get; set; }
        public bool IsSystemMessage { get; set; }
        public string Content { get; set; } = "";
        public string Sentiment { get; set; } = "";
        public string Timestamp { get; set; } = "";
    }
}
