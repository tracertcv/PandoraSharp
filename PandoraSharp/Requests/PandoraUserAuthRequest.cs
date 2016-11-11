using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using PandoraSharp.Exceptions;

namespace PandoraSharp.Requests
{
    [DataContract, KnownType(typeof(PandoraUserAuthRequest))]
    class PandoraUserAuthRequest : PandoraRequest
    {
        //Required URL parameters
        private const string Method = Methods.AuthUserLogin;
        public string PartnerID { get; set; }

        //Disgusting.
        //These properties are required for user auth.
        [DataMember]
        public string loginType { get; set; } = "user";
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string partnerAuthToken { get; set; }
        [DataMember]
        public long syncTime { get; set; }
        //Optional properties
        
        [DataMember]
        public bool returnGenreStations { get; set; } = false;
        [DataMember]
        public bool returnCapped { get; set; } = false;
        [DataMember]
        public bool includePandoraOneInfo { get; set; } = false;
        [DataMember]
        public bool includeDemographics { get; set; } = false;
        [DataMember]
        public bool includeAdAttributes { get; set; } = false;
        [DataMember]
        public bool returnStationList { get; set; } = false;
        [DataMember]
        public bool includeStationArtUrl { get; set; } = false;
        [DataMember]
        public bool includeStationSeeds { get; set; } = false;
        [DataMember]
        public bool includeShuffleInsteadOfQuickMix { get; set; } = false;
        [DataMember]
        public string stationArtSize { get; set; }
        [DataMember]
        public bool returnCollectTrackLifetimeStats { get; set; } = false;
        [DataMember]
        public bool returnIsSubscriber { get; set; } = false;
        [DataMember]
        public bool xplatformAdCapable { get; set; } = false;
        [DataMember]
        public bool complimentarySponsorSupported { get; set; } = false;
        [DataMember]
        public bool includeSubscriptionExpiration { get; set; } = false;
        [DataMember]
        public bool returnHasUsedTrial { get; set; } = false;
        [DataMember]
        public bool returnUserstate { get; set; } = false;
        [DataMember]
        public bool includeAccountMessage { get; set; } = false;
        [DataMember]
        public bool includeUserWebname { get; set; } = false;
        [DataMember]
        public bool includeListeningHours { get; set; } = false;
        [DataMember]
        public bool includeFacebook { get; set; } = false;
        [DataMember]
        public bool includeTwitter { get; set; } = false;
        [DataMember]
        public bool includeDailySkipLimit { get; set; } = false;
        [DataMember]
        public bool includeSkipDelay { get; set; } = false;
        [DataMember]
        public bool includeGoogleplay { get; set; } = false;
        [DataMember]
        public bool includeShowUserRecommendations { get; set; } = false;
        [DataMember]
        public bool includeAdvertiserAttributes { get; set; } = false;
        
        public override string getURLParameters()
        {
            if(partnerAuthToken != null && PartnerID != null)
            {
                return "method=" + Method + "&auth_token=" + Uri.EscapeDataString(partnerAuthToken) + "&partner_id=" + PartnerID;
            }
            throw new PandoraRequestURLFormatException();
        }
    }
}
