using System.Collections.Generic;

namespace PinkRoosterAi.TavilySharp.Models
{
    /// <summary>
    /// Represents the response from a Tavily Search API request.
    /// </summary>
    public class TavilySearchResponse
    {
        public TavilySearchResponse()
        {
            FollowUpQuestions = follow_up_questions ?? new List<string>();
        }
        /// <summary>
        /// The original search query.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// A list of follow-up questions related to the search query.
        /// </summary>
        public List<string> FollowUpQuestions { get; set; }

        /// <summary>
        /// A list of follow-up questions related to the search query.
        /// This property is used for JSON deserialization to match the API response.
        /// </summary>
        public List<string> follow_up_questions { get; set; }

        /// <summary>
        /// A short answer to the original query, if requested.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// A list of query-related image URLs, if requested.
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// A list of search results, sorted by relevancy.
        /// </summary>
        public List<TavilySearchResult> Results { get; set; }

        /// <summary>
        /// The time taken to process the search request.
        /// </summary>
        public float ResponseTime { get; set; }
    }

    /// <summary>
    /// Represents a single search result from the Tavily Search API.
    /// </summary>
    public class TavilySearchResult
    {
        /// <summary>
        /// The title of the search result URL.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The URL of the search result.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The most query-related content from the scraped URL.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The relevance score of the search result.
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// The parsed and cleaned HTML of the site, if requested.
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// The publication date of the source, if available.
        /// </summary>
        public string PublishedDate { get; set; }
    }
}
