using PandoraSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Requests
{
    [DataContract, KnownType(typeof(PandoraGetPlaylistRequest))]
    class PandoraGetPlaylistRequest : PandoraRequest
    {
        private const string Method = Methods.StationGetPlaylist;
        public string PartnerID { get; set; }
        public string UserID { get; set; }

        [DataMember]
        public string userAuthToken { get; set; }
        [DataMember]
        public long syncTime { get; set; }
        [DataMember]
        public string stationToken { get; set; }
        //[DataMember]
        //public string additionalAudioUrl { get; set; }
        [DataMember]
        public bool stationIsStarting { get; set; }
        [DataMember]
        public bool includeTrackLength { get; set; }
        [DataMember]
        public bool includeAudioToken { get; set; }
        [DataMember]
        public bool xplatformAdCapable { get; set; }
        [DataMember]
        public bool includeAudioReceiptUrl { get; set; }
        [DataMember]
        public bool includeBackstageAdUrl { get; set; }
        [DataMember]
        public bool includeSharingUrl { get; set; }
        [DataMember]
        public bool includeSocialAdUrl { get; set; }
        [DataMember]
        public bool includeCompetitiveSepIndicator { get; set; }
        [DataMember]
        public bool includeCompletePlaylist { get; set; }
        [DataMember]
        public bool includeTrackOptions { get; set; }
        [DataMember]
        public bool audioAdPodCapable { get; set; }


        public override string getURLParameters()
        {
            if (userAuthToken != null && UserID != null && PartnerID != null)
            {
                return "method=" + Method + "&auth_token=" + Uri.EscapeUriString(userAuthToken) + "&partner_id=" + PartnerID + "&user_id=" + UserID;
            }
            throw new PandoraRequestURLFormatException();
        }
    }
}
