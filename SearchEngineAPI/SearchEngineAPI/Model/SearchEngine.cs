using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineAPI.Model
{
    public class SearchEngine
    {
        /// <summary>
        /// This will return position of url found in the list returned by google search
        /// </summary>
        public int FoundPosition { get; set; }

        /// <summary>
        /// This will return top 100 url for search text
        /// </summary>
        public List<string> UrlList { get; set; }
    }
}
