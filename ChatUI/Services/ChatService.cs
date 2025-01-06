using ChatUI.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace ChatUI.Services
{
    public class ChatService : IChatService
    {
        private HubConnection? _hubConnection;
        private readonly List<ChatMessage> _messages = new();
        public IReadOnlyList<ChatMessage> Messages => _messages.AsReadOnly();
        public event Action? MessagesUpdated;
        public bool IsConnected { get; private set; } = false;

        public async Task ConnectAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name cannot be empty.", nameof(userName));

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://chatappweb-g6gjcnh5aeg5f5bd.northeurope-01.azurewebsites.net/chatHub")
                .Build();

            _hubConnection.On<string, string, string, string>("ReceiveMessage", (user, message, sentiment, timestamp) =>
            {
                var chatMessage = new ChatMessage
                {
                    UserName = user,
                    Text = message,
                    Sentiment = sentiment,
                    Timestamp = timestamp,
                    IsCurrentUser = user == userName,
                    IsSystemMessage = false
                };
                AddMessage(chatMessage);
            });

            _hubConnection.On<List<ChatMessageDto>>("LoadChatHistory", (chatHistory) =>
            {
                _messages.Clear();
                foreach (var message in chatHistory)
                {
                    _messages.Add(new ChatMessage
                    {
                        UserName = message.UserName,
                        Text = message.Message,
                        Sentiment = message.Sentiment,
                        Timestamp = message.Timestamp.ToString("HH:mm"),
                        IsCurrentUser = message.UserName == userName,
                        IsSystemMessage = false
                    });
                }
                MessagesUpdated?.Invoke();
            });

            _hubConnection.On<string, string>("UserJoined", (user, message) =>
            {
                var systemMessage = new ChatMessage
                {
                    IsSystemMessage = true,
                    Content = message
                };
                AddMessage(systemMessage);
            });

            await _hubConnection.StartAsync();
            IsConnected = true;
            await _hubConnection.InvokeAsync("JoinChat", userName);
        }

        public async Task SendMessageAsync(string userName, string message)
        {
            if (_hubConnection is null || string.IsNullOrWhiteSpace(message))
                throw new InvalidOperationException("Chat is not connected or message is empty.");

            await _hubConnection.InvokeAsync("SendMessage", userName, message);
        }

        public async Task DisconnectAsync(string userName)
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.InvokeAsync("LeaveChat", userName);
                await _hubConnection.DisposeAsync();
            }
            IsConnected = false;
            _messages.Clear();
            MessagesUpdated?.Invoke();
        }

        private void AddMessage(ChatMessage message)
        {
            _messages.Add(message);
            MessagesUpdated?.Invoke();
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                try
                {
                    if (_hubConnection.State == HubConnectionState.Connected)
                    {
                        await _hubConnection.StopAsync();
                    }
                    await _hubConnection.DisposeAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error disposing hubConnection: {ex.Message}");
                }
            }
        }
    }
}
