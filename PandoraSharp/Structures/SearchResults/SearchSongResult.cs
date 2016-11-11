using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Structures.SearchResults
{
    class SearchSongResult : ISearchResult
    {
        public string artistName { get;set; }
        public string musicToken { get; set; }
        public string songName { get; set; }
        public int score { get; set; }

        public string getType()
        {
            return "Song";
        }

        public string getName()
        {
            return songName;
        }
    }
}
