using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            this._chatService = chatService;
        }

        // Sends a message to all connected clients
        public async Task SendMessage(string user, string message)
        {
            var processedMessage = await _chatService.ProcessAndSaveMessageAsync(user, message);

            await Clients.All.SendAsync("ReceiveMessage",
                processedMessage.UserName,
                processedMessage.Message,
                processedMessage.Sentiment,
                processedMessage.Timestamp.ToString("HH:mm"));
        }

        // Loads chat history when a new user connects
        public override async Task OnConnectedAsync()
        {
            var chatHistory = await _chatService.GetChatHistoryAsync();
            await Clients.Caller.SendAsync("LoadChatHistory", chatHistory);

            await base.OnConnectedAsync();
        }

        // Handles a user joining the chat
        public async Task JoinChat(string user)
        {
            await Clients.Others.SendAsync("UserJoined", "System", $"{user} joined the chat.");
            await Clients.Caller.SendAsync("UserJoined", "System", "You joined the chat.");
        }

        // Handles a user leaving the chat
        public async Task LeaveChat(string user)
        {
            await Clients.All.SendAsync("UserJoined", "System", $"{user} left the chat.");
        }
    }
}
