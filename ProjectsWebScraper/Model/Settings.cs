using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsWebScraper.Model
{
    public class Settings
    {
        public string WebSiteUrl { get; set; }
        public string SavePath { get; set; }
        public int ThreadCount { get; set; }
    }
}
