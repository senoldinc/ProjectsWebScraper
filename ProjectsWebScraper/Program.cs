using Microsoft.Extensions.Configuration;
using ProjectsWebScraper.Model;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get values from the config given their key and their target type.
Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

// Write the values to the console.
//Console.WriteLine($"WebSite = {settings.WebSite}");
//Console.WriteLine($"SavePath = {settings.SavePath}");

 Console.WriteLine($"Hi there !!! Let's start to download {settings.WebSite} structure and save to; '{settings.SavePath}' path.");
 Console.WriteLine("If you want to start, please type in any key.");

 var anyKey = Console.ReadLine();

