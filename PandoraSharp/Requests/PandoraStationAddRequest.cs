using PandoraSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Requests
{
    [DataContract, KnownType(typeof(PandoraStationListRequest))]
    class PandoraStationAddRequest : PandoraRequest
    {
        private const string Method = Methods.StationCreateStation;
        public string PartnerID { get; set; }
        public string UserID { get; set; }

        [DataMember]
        public string userAuthToken { get; set; }
        [DataMember]
        public long syncTime { get; set; }
        [DataMember]
        public string trackToken { get; set; }
        [DataMember]
        public string musicType { get; set; }
        [DataMember]
        public string musicToken { get; set; }

        public override string getURLParameters()
        {
            if (userAuthToken != null && UserID != null && PartnerID != null)
            {
                return "method=" + Method + "&auth_token=" + Uri.EscapeDataString(userAuthToken) + "&partner_id=" + PartnerID + "&user_id=" + UserID;
            }
            throw new PandoraRequestURLFormatException();
        }
    }
}
