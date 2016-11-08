using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Structures.SearchResults
{
    class SearchGenreResult : ISearchResult
    {
        public string musicToken { get; set; }
        public int score { get; set; }
        public string stationName { get; set; }
    }
}
