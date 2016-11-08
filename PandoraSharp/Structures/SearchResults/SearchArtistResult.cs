using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Structures.SearchResults
{
    class SearchArtistResult : ISearchResult
    {
        public string artistName { get; set; }
        public string musicToken { get; set; }
        public bool likelyMatch { get; set; }
        public int score { get; set; }
    }
}
