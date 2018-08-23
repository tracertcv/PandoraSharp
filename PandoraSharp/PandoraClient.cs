using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandoraSharp;
using PandoraSharp.Config;
using Newtonsoft.Json;
using PandoraSharp.Requests;
using PandoraSharp.Responses;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using PandoraSharp.Structures.SearchResults;
using PandoraSharp.Structures;
using PandoraSharp.Exceptions;

namespace PandoraSharp
{
    public class PandoraClient
    {
        //Location of config file
        private const string ConfigFile = "\\config.json";

        //Configuration
        private PandoraSharpConfig BaseConfig;
        private PandoraClientConfig ClientConfig;

        //Transportation handler
        private PandoraConnector PConnector { get; set; }
        private const string ProtocolTLS = "https";
        private const string ProtocolNoTLS = "http";

        //Encryption object
        PandoraBlowfish PBlowfish;

        //Station tracking collections
        public PStation CurrentStation { get; set; }
        public List<PStation> StationList { get; set; } 
        public List<ISearchResult> SearchResults { get; set; }     

        //Pandora auth data.  All values need to be calculated from responses.
        private string  PartnerID;
        private string  PartnerAuthToken;
        private string  UserID;
        private string  UserAuthToken;
        private long    SyncTime;

        public PandoraClient()
        {
            this.BaseConfig = loadConfig(ConfigFile);
            this.ClientConfig = BaseConfig.ClientConfig;
            this.PBlowfish = new PandoraBlowfish(ClientConfig.EncryptKey, ClientConfig.DecryptKey);
            this.PConnector = new PandoraConnector(BaseConfig.ConnectorConfig, this.PBlowfish);            
        }

        //Send a partner auth request to Pandora.  This will get us our sync time, partner ID, and partner auth token, which we need for any further requests.
        public bool doPartnerAuth()
        {
            bool responseOK = false;
            PandoraPartnerAuthRequest authRequest = new PandoraPartnerAuthRequest();
            PandoraResponse authResponse;
            authRequest.Protocol = ProtocolTLS;
            authRequest.needEncryption = false;
            authRequest.expectedResponseType = typeof(PandoraPartnerAuthResult);
            authRequest.username = ClientConfig.ParterUser;
            authRequest.password = ClientConfig.PartnerPassword;
            authRequest.deviceModel = ClientConfig.DeviceModel;
            authRequest.version = ClientConfig.Version;
            authRequest.includeUrls = false;
            authRequest.returnDeviceType = false;
            authRequest.returnUpdatePromptVersions = false;
            authResponse = PConnector.doPost(authRequest);
            responseOK = validateResponse(authResponse);
            if(responseOK)
            {
                PandoraPartnerAuthResult authResult = authResponse.result;
                this.PartnerID = authResult.partnerId;
                this.PartnerAuthToken = authResult.partnerAuthToken;
                this.SyncTime = parseSyncTime(authResult.syncTime);
            }
            return responseOK;
        }

        //Send a user auth request to Pandora.  This will get our user auth token and user ID.  Will fail if partner auth not successful.
        public bool doUserAuth()
        {
            bool responseOK = false;
            PandoraUserAuthRequest authRequest = new PandoraUserAuthRequest();
            PandoraResponse authResponse;
            authRequest.Protocol = ProtocolTLS;
            authRequest.expectedResponseType = typeof(PandoraUserAuthResult);
            authRequest.username = ClientConfig.Username;
            authRequest.password = ClientConfig.UserPass;
            authRequest.PartnerID = this.PartnerID;
            authRequest.partnerAuthToken = this.PartnerAuthToken;
            authRequest.syncTime = computeSyncTime();
            authResponse = PConnector.doPost(authRequest);
            responseOK = validateResponse(authResponse);
            if (responseOK)
            {
                PandoraUserAuthResult authResult = authResponse.result;
                this.UserID = authResult.userId;
                this.UserAuthToken = authResult.userAuthToken;
            }
            return responseOK;
        }

        //The following methods require a valid user ID and auth token. 
        //Returns a list of stations currently available to the authed user.
        public List<PStation> doGetStationList()
        {
            bool responseOK = false;
            PandoraStationListRequest stationRequest = new PandoraStationListRequest();
            PandoraResponse stationResponse;
            stationRequest.Protocol = ProtocolNoTLS;
            stationRequest.expectedResponseType = typeof(PandoraStationListResult);
            stationRequest.UserID = this.UserID;
            stationRequest.PartnerID = this.PartnerID;
            stationRequest.userAuthToken = this.UserAuthToken;
            stationRequest.syncTime = computeSyncTime();
            stationResponse = PConnector.doPost(stationRequest);
            responseOK = validateResponse(stationResponse);
            if (responseOK)
            {
                PandoraStationListResult stationResult = stationResponse.result;
                this.StationList = stationResult.stations;
            }
            return this.StationList;
        }

        //Methods for controlling station list.
        //Returns a list of search results.  Pandora returns three different types - artist, song, and genre.  This method aggregates these and returns the full list.
        public List<ISearchResult> doSearchStations(string searchString)
        {
            bool responseOK = false;
            PandoraSearchStationRequest stationRequest = new PandoraSearchStationRequest();
            PandoraResponse stationResponse;
            stationRequest.Protocol = ProtocolNoTLS;
            stationRequest.expectedResponseType = typeof(PandoraSearchStationResult);
            stationRequest.UserID = this.UserID;
            stationRequest.PartnerID = this.PartnerID;
            stationRequest.userAuthToken = this.UserAuthToken;
            stationRequest.syncTime = computeSyncTime();
            stationRequest.searchText = searchString;
            stationResponse = PConnector.doPost(stationRequest);
            responseOK = validateResponse(stationResponse);
            if (responseOK)
            {
                PandoraSearchStationResult stationResult = stationResponse.result;
                this.SearchResults = stationResult.AggregateResults;
            }
            return this.SearchResults;
        }

        //Adds a search result to the station list. Returns the new station.
        public PStation doAddStation(ISearchResult search)
        {
            bool responseOK = false;
            PandoraStationAddRequest addRequest = new PandoraStationAddRequest();
            PandoraResponse addResponse;
            addRequest.Protocol = ProtocolTLS;
            addRequest.expectedResponseType = typeof(PandoraStationAddResult);
            addRequest.UserID = this.UserID;
            addRequest.PartnerID = this.PartnerID;
            addRequest.userAuthToken = this.UserAuthToken;
            addRequest.syncTime = computeSyncTime();
            addRequest.musicType = (search.getType().Equals("Artist")?"artist":"song");
            addRequest.musicToken = search.musicToken;
            addResponse = PConnector.doPost(addRequest);
            responseOK = validateResponse(addResponse);
            if (responseOK)
            {
                PandoraStationAddResult addResult = addResponse.result;
                return addResult.station;
            } else
            {
                throw new PandoraResponseExecption();
            }
        }

        //Returns a list of song objects representing a station's playlist.
        public List<PSong> doGetStationPlaylist(PStation station)
        {
            bool responseOK = false;
            PandoraGetPlaylistRequest playlistRequest = new PandoraGetPlaylistRequest();
            PandoraResponse playlistResponse;
            playlistRequest.Protocol = ProtocolTLS;
            playlistRequest.expectedResponseType = typeof(PandoraGetPlaylistResult);
            playlistRequest.UserID = this.UserID;
            playlistRequest.PartnerID = this.PartnerID;
            playlistRequest.userAuthToken = this.UserAuthToken;
            playlistRequest.syncTime = computeSyncTime();
            playlistRequest.stationToken = station.stationToken;
            playlistResponse = PConnector.doPost(playlistRequest);
            responseOK = validateResponse(playlistResponse);
            if (responseOK)
            {
                PandoraGetPlaylistResult playlistResult = playlistResponse.result;
                return playlistResult.items;
            }
            return new List<PSong>();
        }


        /*
         * Some handy methods
         */
        //Returns true if we are partner authed, false if not.
        public bool isPartnerAuthed()
        {
            return (PartnerID != null && PartnerAuthToken != null); //If we don't have both of these we aren't authed.
        }

        //Returns true if we are user authed, false if not.
        public bool isUserAuthed()
        {
            return (UserID != null && UserAuthToken != null);
        }

        /*
         * Internal methods.  Not for human consumption.
         */

        //Load config
        internal PandoraSharpConfig loadConfig(string filename)
        {
            string config = System.IO.File.ReadAllText(filename);
            PandoraSharpConfig BaseConfig = JsonConvert.DeserializeObject<PandoraSharpConfig>(config);
            return BaseConfig;
        }

        //Parse sync time from Blowfish encrypted string returned by Pandora.  Sync time must be returned to Pandora in all methods other than auth.partnerLogin.
        internal long parseSyncTime(string syncTime)
        {
            string decryptedSyncTime = PBlowfish.Decrypt(syncTime);
            //First six characters are garbage, so strip them out before converting to int
            string strippedSyncTime = decryptedSyncTime.Substring(4, decryptedSyncTime.Length-4);
            long syncTimeAsInt = Convert.ToInt64(strippedSyncTime);
            //Once we have the int result, we need system time in seconds + sync time.
            return now() - syncTimeAsInt;
        }

        //Compute current syncTime value
        internal long computeSyncTime()
        {
            return now() + this.SyncTime;
        }

        //Why the fuck does C# not have an intuitive way to get epoch time?
        internal long now()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            long seconds = (long)t.TotalSeconds; //Don't need milliseconds to calculate sync time.
            return seconds;
        }

        //Method to ensure our request was successful.  Returns true if successful.  If unsuccessful, logs the error and returns false.
        internal bool validateResponse (PandoraResponse response)
        {
            bool isOK = false;
            if (response != null)
            {
                //stat property of response will determine whether or not request was successful. "ok" indicates success.  All other values are fails.
                if (response.stat == "ok")
                {
                    //Response is valid
                    log(String.Format("Request successful. Result type: {0}", response.result.GetType().ToString()));
                    isOK=true;
                }
                else
                {
                    //Response is not valid.
                    log(String.Format("Error occurred while making request: {0}", Errors.getDescription(response.code)));
                    //log(String.Format("Error validating response. Code: {0}, message: {1}, description: {2]", response.code, response.message, Errors.getDescription(response.code)));
                }
            }
            else
            {
                log("Error validating response.  Null response received.");
            }
            return isOK;
        }

        //Logger method
        internal void log(string message)
        {
            Console.WriteLine(message);
        }
    }
}