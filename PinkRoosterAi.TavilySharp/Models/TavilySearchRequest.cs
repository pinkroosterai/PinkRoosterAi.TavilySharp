using System.Collections.Generic;

namespace PinkRoosterAi.TavilySharp.Models
{
    /// <summary>
    /// Represents a search request to the Tavily Search API.
    /// </summary>
    public class TavilySearchRequest
    {
        /// <summary>
        /// The search query to execute with Tavily.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The depth of the search. Can be "basic" or "advanced".
        /// </summary>
        public string SearchDepth { get; set; } = "basic";

        /// <summary>
        /// The category of the search. Currently supports "general" and "news".
        /// </summary>
        public string Topic { get; set; } = "general";

        /// <summary>
        /// The number of days back from the current date to include in the search results.
        /// </summary>
        public int? Days { get; set; }

        /// <summary>
        /// The maximum number of search results to return.
        /// </summary>
        public int MaxResults { get; set; } = 5;

        /// <summary>
        /// Whether to include a list of query-related images in the response.
        /// </summary>
        public bool IncludeImages { get; set; } = false;

        /// <summary>
        /// Whether to include a short answer to the original query.
        /// </summary>
        public bool IncludeAnswer { get; set; } = false;

        /// <summary>
        /// Whether to include the cleaned and parsed HTML content of each search result.
        /// </summary>
        public bool IncludeRawContent { get; set; } = false;

        /// <summary>
        /// A list of domains to specifically include in the search results.
        /// </summary>
        public List<string> IncludeDomains { get; set; } = new List<string>();

        /// <summary>
        /// A list of domains to specifically exclude from the search results.
        /// </summary>
        public List<string> ExcludeDomains { get; set; } = new List<string>();
    }
}
