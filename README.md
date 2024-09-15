# 🚀 TavilySharp: Unofficial Tavily C# Library

Welcome to TavilySharp, the unofficial C# library for the Tavily Search API! 🎉 This library provides a simple and efficient way to interact with Tavily's powerful search capabilities. Let's dive in and explore the wealth of information at your fingertips! 🌌

## ✨ Features

- 🔍 Perform basic and advanced searches
- 🤔 Ask questions (QnA functionality)
- 📚 Retrieve search contexts
- ⚙️ Configurable search options

## 🚀 Usage

Here's a quick example to get you started on your search adventure:

```csharp
using PinkRoosterAi.TavilySharp.Client;

// Initialize the client with your API key
var client = new TavilyClient("YOUR_API_KEY");

// Perform a basic search
var searchResponse = await client.SearchAsync("What is the meaning of life?");

// Ask a question
var answer = await client.QnaAsync("Who wrote The Hitchhiker's Guide to the Galaxy?");

// Get search context
var context = await client.GetSearchContextAsync("Tell me about the Vogons", maxTokens: 1000);
```

For more detailed examples, check out the `PinkRoosterAi.TavilySharp.Example` project in this repository. It's a great resource for understanding how to use the library effectively! 🗺️

## 🛠️ Configuration

Customize your search experience with these options:

- `searchDepth`: Choose between "basic" or "advanced" for your search depth 🌠
- `includeImages`: Set to true to include images in search results 🖼️
- `includeAnswer`: Get a direct answer to your questions 🔥
- `maxResults`: Control the number of results returned 🌊

Additional options:

- `includeDomains`: Specify domains to include in the search 🌐
- `excludeDomains`: Specify domains to exclude from the search 🚫

## 🐛 Error Handling

Our library comes with custom exceptions to help you handle API-related issues gracefully. Wrap your code in try-catch blocks and handle `TavilyException` for API-specific errors:

```csharp
try
{
    var result = await client.SearchAsync("Your query");
    // Process the result
}
catch (TavilyException ex)
{
    Console.WriteLine($"Tavily API error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
}
```

## 📜 License

This project is licensed under the MIT License. Feel free to use it, modify it, and share it with the world! 🌍

## ⚠️ Disclaimer

Please note that this is an unofficial library and is not endorsed by Tavily. Use it at your own discretion and risk. We are not responsible for any issues that may arise from using this software.

Happy searching, and may your queries always return meaningful results! 🚀🌌

