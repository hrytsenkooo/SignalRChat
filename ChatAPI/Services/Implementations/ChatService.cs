using AutoMapper;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _repository;
        private readonly ISentimentAnalysisService _analysisService;
        private readonly IMapper _mapper;

        public ChatService(IChatMessageRepository repository, ISentimentAnalysisService analysisService, IMapper mapper)
        {
            this._repository = repository;
            this._analysisService = analysisService;
            this._mapper = mapper;
        }

        // Retrieves chat history and maps it to a DTO
        public async Task<List<ChatMessageDto>> GetChatHistoryAsync()
        {
            var messages = await _repository.GetChatHistoryAsync();
            return _mapper.Map<List<ChatMessageDto>>(messages); 
        }

        // Processes a message (including sentiment analysis) and saves it to the repository
        public async Task<ChatMessageDto> ProcessAndSaveMessageAsync(string userName, string message)
        {
            var sentiment = await _analysisService.AnalyzeSentimentAsync(message);
            var chatMessage = new ChatMessage
            {
                UserName = userName,
                Message = message,
                Sentiment = sentiment,
                Timestamp = DateTime.Now
            };

            await _repository.SaveMessageAsync(chatMessage);
            return _mapper.Map<ChatMessageDto>(chatMessage);
        }
    }
}
