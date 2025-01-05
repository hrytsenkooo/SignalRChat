using ChatAPI.Models;

namespace ChatAPI.Repositories.Interfaces
{
    public interface IChatMessageRepository
    {
        Task SaveMessageAsync(ChatMessage chatMessage);
        Task<List<ChatMessage>> GetChatHistoryAsync();
    }
}
