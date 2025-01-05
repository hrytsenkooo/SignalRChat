using ChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Data
{
    public class ChatDbContext : DbContext
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
    }
}
