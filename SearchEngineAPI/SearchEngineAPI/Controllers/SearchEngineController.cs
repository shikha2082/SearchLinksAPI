using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngineAPI.BusinessLogic;
using SearchEngineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchEngineController : ControllerBase
    {
        [HttpGet("search")]
        public JsonResult Get(string searchText, string searchUrl)
        {
            SearchEngine searchEngine = null;
            if(!string.IsNullOrEmpty(searchText))
            {
                SearchEngineBL searchEngineBL = new SearchEngineBL();
                searchEngine = searchEngineBL.StartSearch(searchText, searchUrl);
            }
            return new JsonResult(searchEngine);
        }

        [HttpGet]
        public JsonResult GetResult()
        {
            return new JsonResult("test get");
        }
    }
}
