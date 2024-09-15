using System.Collections.Generic;

namespace PinkRoosterAi.TavilySharp.Models
{
    /// <summary>
    /// Represents additional options for Tavily search requests.
    /// </summary>
    public class TavilySearchOptions
    {
        /// <summary>
        /// The depth of the search. It can be "basic" or "advanced".
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
        /// A list of domains to specifically include in the search results.
        /// </summary>
        public List<string> IncludeDomains { get; set; }

        /// <summary>
        /// A list of domains to specifically exclude from the search results.
        /// </summary>
        public List<string> ExcludeDomains { get; set; }
    }
}
