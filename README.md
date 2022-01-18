# ProjectsWebScraper
Project web scraper is c# .net console application. Its can be recursively traverse and download the web site structure and save it to disk.  

*Please be aware that ProjectsWebScraper are only compatible with .NET 6.0.* 

### This projects have a 3 Nuget packages ###

From the package manager console: 

```powershell
Install-Package Microsoft.Extensions.Configuration.Binder
Install-Package Microsoft.Extensions.Configuration.Json
Install-Package Microsoft.Extensions.Configuration.EnvironmentVariables
```
### Configuration App ###

This app have a appsettings.json file witch help to settings app configuration:

```Json
{
  "Settings": {
    "WebSiteUrl": "https://tretton37.com/",
    "SavePath": "C:\\Users\\SENOL\\Documents\\Scraping",
    "ThreadCount":  2
  } 
 }
```
