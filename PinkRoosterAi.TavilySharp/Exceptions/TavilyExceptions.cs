using System;

namespace PinkRoosterAi.TavilySharp.Exceptions
{
    /// <summary>
    /// Represents errors that occur during Tavily API operations.
    /// </summary>
    public class TavilyException : Exception
    {
        public TavilyException() { }
        public TavilyException(string message) : base(message) { }
        public TavilyException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Represents errors that occur during Tavily API requests.
    /// </summary>
    public class TavilyApiException : TavilyException
    {
        public TavilyApiException() { }
        public TavilyApiException(string message) : base(message) { }
        public TavilyApiException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when the API key is missing.
    /// </summary>
    public class MissingAPIKeyException : TavilyException
    {
        public MissingAPIKeyException() : base("API key is missing. Please provide a valid API key.") { }
    }

    /// <summary>
    /// Thrown when the provided API key is invalid.
    /// </summary>
    public class InvalidAPIKeyException : TavilyException
    {
        public InvalidAPIKeyException() : base("Invalid API key provided. Please check your API key.") { }
    }

    /// <summary>
    /// Thrown when the usage limit for the API has been exceeded.
    /// </summary>
    public class UsageLimitExceededException : TavilyException
    {
        public UsageLimitExceededException() : base("Usage limit exceeded. Please check your plan's usage limits or consider upgrading.") { }
    }
}
