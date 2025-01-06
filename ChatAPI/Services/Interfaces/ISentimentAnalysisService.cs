namespace ChatAPI.Services.Interfaces
{
    public interface ISentimentAnalysisService
    {
        // Method to analyze the sentiment of a message asynchronously
        Task<string> AnalyzeSentimentAsync(string message);
    }
}
