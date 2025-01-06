using ChatUI.Models;

namespace ChatUI.Services
{
    public interface IChatService : IAsyncDisposable
    {
        // Property to get the list of chat messages
        IReadOnlyList<ChatMessage> Messages { get; }

        // Event that is triggered when messages are updated
        event Action? MessagesUpdated;

        // Property indicating if the client is currently connected
        bool IsConnected { get; }

        // Connects the client to the chat service with the specified user name
        Task ConnectAsync(string userName);

        // Sends a message from the specified user
        Task SendMessageAsync(string userName, string message);

        // Disconnects the specified user from the chat service
        Task DisconnectAsync(string userName);
    }
}
