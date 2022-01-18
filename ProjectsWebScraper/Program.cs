using Microsoft.Extensions.Configuration;
using ProjectsWebScraper.Model;
using System.Net;
using System.Text.RegularExpressions;


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

 Console.WriteLine($"Hi there !!! Let's start to download {settings.WebSiteUrl} structure and save to; '{settings.SavePath}' path.");
 Console.WriteLine("If you want to start, please type in any key.");

 var anyKey = Console.ReadLine();

if (!string.IsNullOrEmpty(anyKey))
{
    //Lets start to download.
    await DownloadFile(settings.WebSiteUrl, settings.SavePath, "Index", settings.ThreadCount);
}

/// <summary>
/// <param name="url">Target scrapping web site adress.</param> 
/// <param name="savePath"> Directory to saving structure </param>
/// <param name="saveName"> Saving file name </param>
/// <param name="threadCount"> Want to be use the count of the core for machine </param>
/// </summary>
static async Task DownloadFile(string url, string savePath, string saveName, int threadCount)
{
    using (WebClient client = new WebClient())
    {
        var pageContent = await client.DownloadStringTaskAsync(url);
        Console.WriteLine($"{saveName} file start to downloading...");
        var filePath = Path.Combine(savePath, string.Format("{0}.html", saveName));
        var file = new FileInfo(filePath);
        if (!file.Directory.Exists)
        {
            file.Directory.Create();
        }
        await File.WriteAllTextAsync(file.FullName, pageContent);
        Console.WriteLine($"{saveName} finish the download and save to disk.");

        if (saveName == "Index")
        {
            MatchCollection listingMatches = Regex.Matches(pageContent, @"<li><a href\s*=\s*(?:[""'](?<LINK>[^""']*)[""']|(?<1>[^>\s]+))");
           
            int totalPageCount = (int)Math.Ceiling((double)listingMatches.Count / threadCount);
            for (int page = 0; page < totalPageCount; page++)
            {
                var matchBatch = listingMatches.Skip(page * threadCount).Take(threadCount);
                Parallel.ForEach(matchBatch, async m =>
                {
                    await DownloadFile(String.Format("{0}{1}", url, m.Groups["LINK"].Value.ToString()), savePath, m.Groups["LINK"].Value.ToString().Replace("/", ""), threadCount);
                });
            }
           
        }
      
    }
}