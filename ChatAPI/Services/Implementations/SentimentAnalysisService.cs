using Azure.AI.TextAnalytics;
using Azure;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services.Implementations
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly TextAnalyticsClient _client;

        public SentimentAnalysisService(string endpoint, string apiKey)
        {
            var credentials = new AzureKeyCredential(apiKey);
            var client = new Uri(endpoint);
            _client = new TextAnalyticsClient(client, credentials);
        }

        // Analyzes the sentiment of the given message and returns the sentiment as a string
        public async Task<string> AnalyzeSentimentAsync(string message)
        {
            var response = await _client.AnalyzeSentimentAsync(message);
            return response.Value.Sentiment.ToString();
        }
    }
}
