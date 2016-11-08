using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Responses
{
    class PandoraUserAuthResult : IPandoraResponseResult
    {
        public string stationCreationAdUrl { get; set; }
        public bool hasAudioAds { get; set; }
        public string splashScreenAdUrl { get; set; }
        public string videoAdUrl { get; set; }
        public string username { get; set; }
        public bool canListen { get; set; }
        public string nowPlayingAdUrl { get; set; }
        public string userId { get; set; }
        public string listeningTimeoutMinutes { get; set; }
        public int maxStationsAllowed { get; set; }
        public string listeningTimeoutAlertMsgUri { get; set; }
        public string userProfileUrl { get; set; }
        public int minimumAdRefreshInterval { get; set; }
        public string userAuthToken { get; set; }

        public void populate(string data)
        {
            JsonConvert.PopulateObject(data, this);
        }
    }
}
