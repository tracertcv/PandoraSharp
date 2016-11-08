using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Responses
{
    class PandoraResponse
    {
        public string stat { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string message { get; set; }
        [JsonProperty(Required = Required.Default)]
        public int code { get; set; }
        [JsonProperty(Required = Required.Default)]
        public dynamic result { get; set; }
    }
}
