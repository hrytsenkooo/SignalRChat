using AutoMapper;
using ChatAPI.Models;

namespace ChatAPI.Mappings
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            CreateMap<ChatMessage, ChatMessageDto>()
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.ToString("HH:mm")));
        }
    }
}
