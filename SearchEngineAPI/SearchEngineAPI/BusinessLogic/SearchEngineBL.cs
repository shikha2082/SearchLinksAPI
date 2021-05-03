using HtmlAgilityPack;
using NUnit.Framework;
using SearchEngineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineAPI.BusinessLogic
{
    public class SearchEngineBL
    {
        public SearchEngine StartSearch(string searchText, string searchURL)
        {
            SearchEngine searchEngine = new SearchEngine();
            List<string> searchResults = GetSearchResults(searchText);
            if(searchResults != null && searchResults.Count > 0)
            {
                searchEngine.UrlList = searchResults;
                int searchCounter = 0;
                foreach(var search in searchResults)
                {
                    searchCounter++;
                    if(search == searchURL)
                    {
                        break;
                    }
                }

                searchEngine.FoundPosition = searchCounter;
            }
            return searchEngine;
        }

        private List<string> GetSearchResults(string searchText)
        {
            List<string> urls = new List<string>();

            String requesturl = $"https://www.google.co.uk/search?num=100&q={searchText}";
            try
            {
                var result = new HtmlWeb().Load(requesturl);
                var nodes = result.DocumentNode.SelectNodes("//html//body//div[@class='g']");

                var innerDiv = nodes[0].SelectNodes("//div[@class='tF2Cxc']//div[@class='yuRUbf']");
                var innerhtml = innerDiv == null ? null :
                    innerDiv.Select(x => x.InnerHtml).ToList();

                if (innerhtml != null)
                {
                    var doc = new HtmlDocument();
                    foreach (var a in innerhtml)
                    {
                        doc.LoadHtml(a);
                        var node = doc.DocumentNode.SelectNodes("a[@href]").FirstOrDefault();
                        var url = node.Attributes[0].Value;

                        urls.Add(url);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return urls;
        }

        [Fact]
        private void TestSearchResultsMethod()
        {
            //Act
            List<string> results = GetSearchResults("spring web");

            //Assert
            Assert.IsNotNull(results, "Invalid Result");
            Assert.LessOrEqual(100, results.Count, "Invalid Result");

            //Act
            results = GetSearchResults("");

            //Assert
            Assert.IsNull(results, "Invalid Result");
        }
    }
}
