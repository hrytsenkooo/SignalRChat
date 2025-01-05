using ChatAPI.Data;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Repositories.Implementations
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ChatDbContext _context;
        private const int MessageHistoryLimit = 50;

        public ChatMessageRepository(ChatDbContext context)
        {
            this._context = context;
        }

        public async Task<List<ChatMessage>> GetChatHistoryAsync()
        {
            return await _context.ChatMessages
                .OrderByDescending(m => m.Timestamp)
                .Take(MessageHistoryLimit)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task SaveMessageAsync(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }
    }
}
