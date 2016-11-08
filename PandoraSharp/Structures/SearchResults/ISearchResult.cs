using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Structures.SearchResults
{
    interface ISearchResult
    {
        string musicToken { get; set; }
        int score { get; set; }
    }
}
