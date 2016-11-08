using Newtonsoft.Json;
using PandoraSharp.Structures.SearchResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Responses
{
    class PandoraSearchStationResult : IPandoraResponseResult
    {
        public bool nearMatchesAvailable { get; set; }
        public string explanation { get; set; }
        public List<SearchSongResult> songs { get; set; }
        public List<SearchArtistResult> artists { get; set; }
        public List<SearchGenreResult> genreStations { get; set;}

        public void populate(string data)
        {
            JsonConvert.PopulateObject(data, this);
        }
    }
}
