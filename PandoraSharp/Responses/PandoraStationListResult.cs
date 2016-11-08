using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Responses
{
    class PandoraStationListResult : IPandoraResponseResult
    {
        public List<PStation> stations { get; set; }
        public string checksum { get; set; }

        public void populate(string data)
        {
            JsonConvert.PopulateObject(data, this);
        }
    }
}
