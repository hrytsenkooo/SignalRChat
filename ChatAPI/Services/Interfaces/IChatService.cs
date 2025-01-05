using ChatAPI.Models;

namespace ChatAPI.Services.Interfaces
{
    public interface IChatService
    {
        Task<ChatMessageDto> ProcessAndSaveMessageAsync(string userName, string message);
        Task<List<ChatMessageDto>> GetChatHistoryAsync();
    }
}
