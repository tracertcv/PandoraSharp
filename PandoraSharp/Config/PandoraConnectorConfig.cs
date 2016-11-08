using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PandoraSharp.Config
{
    [DataContract]
    class PandoraConnectorConfig
    {
        //API endpoint and protocol data
        [DataMember]
        public string ProtocolTLS { get; set; } = "https";
        [DataMember]
        public string ProtocolNonTLS { get; set; } = "http";
        [DataMember]
        public string BaseURL { get; set; } = "://tuner.pandora.com/services/json/?";
        
        //Header settings for HTTP requests
        [DataMember]
        public string RequestMethod = "POST";
        [DataMember]
        public string RequestUserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
        [DataMember]
        public string RequestContentType = "text/plain";
        [DataMember]
        public string RequestAccept = "*/*";
        public PandoraConnectorConfig() { }
    }
}