using Newtonsoft.Json;
using PandoraSharp.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Responses
{
    class PandoraStationAddResult : IPandoraResponseResult
    {
        public PStation station { get; set; }
        public string checksum { get; set; }

        public void populate(string data)
        {
            station = new PStation();
            JsonConvert.PopulateObject(data, station);
        }
    }
}
