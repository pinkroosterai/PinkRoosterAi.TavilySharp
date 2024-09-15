using System;
using System.Threading.Tasks;
using PinkRoosterAi.TavilySharp.Client;
using PinkRoosterAi.TavilySharp.Exceptions;

namespace PinkRoosterAi.TavilySharp.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Star Trek Tavily Search API Example");

            string apiKey = Environment.GetEnvironmentVariable("TAVILY_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("TAVILY_API_KEY environment variable is not set.");
                Console.Write("Please enter your Tavily API key: ");
                apiKey = Console.ReadLine();
                if (string.IsNullOrEmpty(apiKey))
                {
                    Console.WriteLine("Error: No API key provided. Exiting.");
                    return;
                }
            }

            var client = new TavilyClient(apiKey);

            try
            {
                // Example 1: Basic Search
                Console.WriteLine("\nPerforming a basic search about Captain Picard...");
                var searchResponse = await client.SearchAsync("Who is Captain Jean-Luc Picard?");
                Console.WriteLine($"Answer: {searchResponse.Answer}");
                Console.WriteLine("Search Results:");
                foreach (var result in searchResponse.Results)
                {
                    Console.WriteLine($"- {result.Title}: {result.Url}");
                }

                // Example 2: QnA
                Console.WriteLine("\nAsking about the Prime Directive...");
                var qnaAnswer = await client.QnaAsync("What is the Prime Directive in Star Trek?");
                Console.WriteLine($"Answer: {qnaAnswer}");

                // Example 3: Get Search Context
                Console.WriteLine("\nGetting context about the Klingon Empire...");
                var context = await client.GetSearchContextAsync("The history of the Klingon Empire", 1000);
                Console.WriteLine("Context:");
                Console.WriteLine(context);
            }
            catch (TavilyException ex)
            {
                Console.WriteLine($"Tavily API error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
