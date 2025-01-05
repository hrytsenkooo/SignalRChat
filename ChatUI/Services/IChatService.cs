using ChatUI.Models;

namespace ChatUI.Services
{
    public interface IChatService : IAsyncDisposable
    {
        IReadOnlyList<ChatMessage> Messages { get; }
        event Action? MessagesUpdated;
        bool IsConnected { get; }
        Task ConnectAsync(string userName);
        Task SendMessageAsync(string userName, string message);
        Task DisconnectAsync(string userName);
    }
}
