using System;
using System.Runtime.Serialization;

namespace PandoraSharp.Requests
{
    [DataContract, KnownType(typeof(PandoraPartnerAuthRequest))]
    class PandoraPartnerAuthRequest : PandoraRequest
    {
        //Required URL parameters
        private const string Method = Methods.AuthPartnerLogin;
            
        //JSON properties
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string deviceModel { get; set; }
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public Boolean includeUrls { get; set; }
        [DataMember]
        public Boolean returnDeviceType { get; set; }
        [DataMember]
        public Boolean returnUpdatePromptVersions { get; set; }
            
        public PandoraPartnerAuthRequest()
        {

        }

        public override string getURLParameters()
        {
            return "method=" + Method;
        }

    }
}
