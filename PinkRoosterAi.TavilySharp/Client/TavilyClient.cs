using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using PinkRoosterAi.TavilySharp.Exceptions;
using PinkRoosterAi.TavilySharp.Models;
using PinkRoosterAi.TavilySharp.Configuration;

namespace PinkRoosterAi.TavilySharp.Client
{
    /// <summary>
    /// Client for interacting with the Tavily Search API.
    /// </summary>
    public class TavilyClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.tavily.com/";


        /// <summary>
        /// Initializes a new instance of the TavilyClient class.
        /// </summary>
        /// <param name="apiKey">The API key for authenticating with Tavily.</param>
        /// <param name="httpClient">Optional HttpClient instance. If not provided, a new one will be created.</param>
        /// <example>
        /// This example shows how to create a new TavilyClient for searching Star Trek information:
        /// <code>
        /// var apiKey = "your-tavily-api-key-here";
        /// var starTrekClient = new TavilyClient(apiKey);
        /// </code>
        /// </example>
        public TavilyClient(string apiKey, HttpClient httpClient = null)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("API key is required.", nameof(apiKey));
            }
            _apiKey = apiKey;
            _httpClient = httpClient ?? new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        /// <summary>
        /// Performs a search using the Tavily Search API.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="searchDepth">The depth of the search. Can be "basic" or "advanced".</param>
        /// <param name="includeImages">Whether to include images in the search results.</param>
        /// <param name="includeAnswer">Whether to include an answer in the search results.</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <param name="apiKey">Optional API key to override the one set in the constructor.</param>
        /// <returns>A TavilySearchResponse containing the search results.</returns>
        /// <example>
        /// This example shows how to perform a basic search about Star Trek:
        /// <code>
        /// var client = new TavilyClient("your-tavily-api-key");
        /// var response = await client.SearchAsync("Who is Captain Jean-Luc Picard?");
        /// foreach (var result in response.Results)
        /// {
        ///     Console.WriteLine($"Title: {result.Title}");
        ///     Console.WriteLine($"URL: {result.Url}");
        ///     Console.WriteLine($"Content: {result.Content}");
        ///     Console.WriteLine();
        /// }
        /// </code>
        /// </example>
        public async Task<TavilySearchResponse> SearchAsync(
            string query,
            string searchDepth = "basic",
            bool includeImages = false,
            bool includeAnswer = false,
            int maxResults = 5,
            string apiKey = null)
        {
            var request = new TavilySearchRequest
            {
                Query = query,
                SearchDepth = searchDepth,
                IncludeImages = includeImages,
                IncludeAnswer = includeAnswer,
                MaxResults = maxResults
            };

            return await ExecuteRequestAsync(request, apiKey);
        }

        /// <summary>
        /// Performs a question-answering search using the Tavily Search API.
        /// </summary>
        /// <param name="query">The question to be answered.</param>
        /// <param name="searchDepth">The depth of the search. Can be "basic" or "advanced".</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <param name="apiKey">Optional API key to override the one set in the constructor.</param>
        /// <returns>A string containing the answer to the question.</returns>
        /// <example>
        /// This example shows how to use the QnA feature for Star Trek information:
        /// <code>
        /// var client = new TavilyClient("your-tavily-api-key");
        /// var answer = await client.QnaAsync("What is the Prime Directive in Star Trek?");
        /// Console.WriteLine($"Answer: {answer}");
        /// </code>
        /// </example>
        public async Task<string> QnaAsync(
            string query,
            string searchDepth = "advanced",
            int maxResults = 5,
            string apiKey = null)
        {
            var request = new TavilySearchRequest
            {
                Query = query,
                SearchDepth = searchDepth,
                IncludeAnswer = true,
                MaxResults = maxResults
            };

            var response = await ExecuteRequestAsync(request, apiKey);
            return response.Answer ?? "No answer found.";
        }

        /// <summary>
        /// Performs a search and returns a string of content and sources within the provided token limit.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="maxTokens">The maximum number of tokens for the response. Default is 4000.</param>
        /// <param name="searchDepth">The depth of the search. Can be "basic" or "advanced".</param>
        /// <param name="apiKey">Optional API key to override the one set in the constructor.</param>
        /// <returns>A string containing the content and sources of the results.</returns>
        /// <example>
        /// This example shows how to get search context about Star Trek:
        /// <code>
        /// var client = new TavilyClient("your-tavily-api-key");
        /// var context = await client.GetSearchContextAsync("The history of the Klingon Empire", 2000);
        /// Console.WriteLine(context);
        /// </code>
        /// </example>
        public async Task<string> GetSearchContextAsync(
            string query,
            int maxTokens = 4000,
            string searchDepth = "basic",
            string apiKey = null)
        {
            var request = new TavilySearchRequest
            {
                Query = query,
                SearchDepth = searchDepth,
                MaxResults = 10  // Increased to get more potential content
            };

            var response = await ExecuteRequestAsync(request, apiKey);

            // TODO: Implement token counting and truncation logic
            return string.Join("\n\n", response.Results.Select(r => $"{r.Content}\nSource: {r.Url}"));
        }

        private async Task<TavilySearchResponse> ExecuteRequestAsync(TavilySearchRequest request, string apiKey = null)
        {
            var requestBody = CreateRequestBody(request, apiKey);

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"search", requestBody);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var searchResponse = JsonSerializer.Deserialize<TavilySearchResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return searchResponse ?? throw new TavilyException("Failed to deserialize the response.");
            }
            catch (HttpRequestException e)
            {
                throw new TavilyApiException($"HTTP request failed: {e.Message}", e);
            }
            catch (JsonException e)
            {
                throw new TavilyApiException($"Failed to parse the API response: {e.Message}", e);
            }
        }

        private object CreateRequestBody(TavilySearchRequest request, string apiKey = null)
        {
            return new
            {
                api_key = apiKey ?? _apiKey,
                query = request.Query,
                search_depth = request.SearchDepth,
                include_images = request.IncludeImages,
                include_answer = request.IncludeAnswer,
                max_results = request.MaxResults,
                include_domains = request.IncludeDomains,
                exclude_domains = request.ExcludeDomains
            };
        }
    }
}
