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

        public List<ISearchResult> AggregateResults { get; set; }

        public void populate(string data)
        {
            //Make sure we don't run into any null references when we aggregate the results.
            songs = new List<SearchSongResult>();
            artists = new List<SearchArtistResult>();
            genreStations = new List<SearchGenreResult>();
            JsonConvert.PopulateObject(data, this);
            aggregateResults();
        }

        private void aggregateResults()
        {
            List<ISearchResult> results = new List<ISearchResult>();
            foreach(SearchSongResult song in songs)
            {
                ISearchResult agSong = song;
                results.Add(agSong);
            }
            foreach (SearchArtistResult artist in artists)
            {
                ISearchResult agArtist = artist;
                results.Add(agArtist);
            }
            foreach (SearchGenreResult genre in genreStations)
            {
                ISearchResult agGenre = genre;
                results.Add(agGenre);
            }
            AggregateResults = results;
        }
    }
}
