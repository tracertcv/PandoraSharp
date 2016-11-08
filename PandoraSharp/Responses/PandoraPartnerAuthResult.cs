using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PandoraSharp.Responses
{
    class PandoraPartnerAuthResult : IPandoraResponseResult
    {
        public string syncTime { get; set; }
        //TODO: fix this.  Need to figure out how to recursively parse unknown depth.  Device properties are probably unnecessary for any use cases of this library.
        //public Dictionary<string,string> deviceProperties { get; set; }
        public string partnerAuthToken { get; set; }
        public string partnerId { get; set; }
        public string stationSkipUnit { get; set; }
        public Dictionary<string,string> urls { get; set; }
        public int stationSkipLimit { get; set; }

        public void populate(string data)
        {
            JsonConvert.PopulateObject(data, this);
        }
    }
}
