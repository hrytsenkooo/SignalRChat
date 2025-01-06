using ChatAPI.Models;

namespace ChatAPI.Services.Interfaces
{
    public interface IChatService
    {
        // Method to process and save a message asynchronously
        Task<ChatMessageDto> ProcessAndSaveMessageAsync(string userName, string message);
        // Method to retrieve the chat history asynchronously
        Task<List<ChatMessageDto>> GetChatHistoryAsync();
    }
}
