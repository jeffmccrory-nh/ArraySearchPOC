using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArraySearchPOC.Helpers;
using ArraySearchPOC.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArraySearchPOC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchArrayController : ControllerBase
    {
        private readonly ILogger<SearchArrayController> _logger;
        private readonly WordGenerator _wordGenerator = new();
        private readonly SearchTermFinder _searchTermFinder = new();
        private List<string> PageOfWords { get; set; } = new();
        public SearchArrayController(ILogger<SearchArrayController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        [Route("SearchTerm")]
        public Dictionary<string, List<Mention>> SearchArray(string searchTerm)
        {
            // Create SearchTerm Collection (as it will be used for concept list in the future)
            var searchTerms = new Dictionary<string, List<Mention>>();
            // list of concepts would normally be loaded into the searchTerms collection here
            searchTerms.Add(searchTerm, new List<Mention>());
            searchTerms.Add("find this", new List<Mention>());
            searchTerms.Add("this", new List<Mention>());
            //Load Document Words
            _searchTermFinder.Init(GeneratePageOfWords(25000).ToList());

            // Call Finder with Terms(Concepts)
            foreach (var searchTermInstance in searchTerms)
            {
                var processedTerm = _searchTermFinder.FindSearchTerm(searchTermInstance);
                searchTerms[processedTerm.Key] = processedTerm.Value;

            }
            // Return SearchTerm List with Mentions
            return searchTerms;
            
        }

        private List<string> GeneratePageOfWords(int noOfWords)
        {
            for (int i = 0; i < noOfWords; i++)
            {
                if (i is 15000 or 20000 or 24000)
                {
                    PageOfWords.Add("find");
                    PageOfWords.Add("this");
                }
                PageOfWords.Add(_wordGenerator.GenerateWord());
            }
            return PageOfWords;
        }
    }
}
