namespace ChatAPI.Services.Interfaces
{
    public interface ISentimentAnalysisService
    {
        Task<string> AnalyzeSentimentAsync(string message);
    }
}
