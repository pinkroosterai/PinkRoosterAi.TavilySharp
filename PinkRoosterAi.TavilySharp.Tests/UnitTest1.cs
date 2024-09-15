using System;
using System.Threading.Tasks;
using Xunit;
using PinkRoosterAi.TavilySharp.Client;
using PinkRoosterAi.TavilySharp.Exceptions;
using PinkRoosterAi.TavilySharp.Models;

namespace PinkRoosterAi.TavilySharp.Tests
{
    public class TavilyClientTests
    {
        private readonly string _apiKey;

        public TavilyClientTests()
        {
            _apiKey = Environment.GetEnvironmentVariable("TAVILY_API_KEY");
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("TAVILY_API_KEY environment variable is not set.");
            }
        }

        [Fact]
        public async Task SearchAsync_ValidQuery_ReturnsResults()
        {
            // Arrange
            var client = new TavilyClient(_apiKey);
            var query = "Who is Captain Jean-Luc Picard?";

            // Act
            var response = await client.SearchAsync(query);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Results);
            Assert.NotEmpty(response.Results);
            Assert.Contains(response.Results, r => r.Title.Contains("Picard", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task QnaAsync_ValidQuestion_ReturnsAnswer()
        {
            // Arrange
            var client = new TavilyClient(_apiKey);
            var question = "What is the Prime Directive in Star Trek?";

            // Act
            var answer = await client.QnaAsync(question);

            // Assert
            Assert.NotNull(answer);
            Assert.Contains("Prime Directive", answer, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task GetSearchContextAsync_ValidQuery_ReturnsContext()
        {
            // Arrange
            var client = new TavilyClient(_apiKey);
            var query = "The history of the Klingon Empire";
            var maxTokens = 1000;

            // Act
            var context = await client.GetSearchContextAsync(query, maxTokens);

            // Assert
            Assert.NotNull(context);
            Assert.Contains("Klingon", context, StringComparison.OrdinalIgnoreCase);
            Assert.True(context.Length <= maxTokens);
        }

        [Fact]
        public async Task SearchAsync_InvalidApiKey_ThrowsTavilyException()
        {
            // Arrange
            var client = new TavilyClient("invalid_api_key");
            var query = "Test query";

            // Act & Assert
            await Assert.ThrowsAsync<TavilyException>(() => client.SearchAsync(query));
        }
    }
}
