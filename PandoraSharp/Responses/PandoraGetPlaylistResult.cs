using Newtonsoft.Json;
using PandoraSharp.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Responses
{
    class PandoraGetPlaylistResult : IPandoraResponseResult
    {
        public List<PSong> items { get; set; }

        public void populate(string data)
        {
            JsonConvert.PopulateObject(data, this);
        }
    }
}
