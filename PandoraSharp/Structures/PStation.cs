using System.Collections.Generic;

namespace PandoraSharp.Structures
{
    public class PStation
    {
        public bool suppressVideoAds { get; set; }
        public bool isQuickMix { get; set; }
        public string stationId { get; set; }
        public string stationDetailUrl { get; set; }
        public bool isShared { get; set; }
        public Dictionary<string,long> dateCreated { get; set; }
        public string stationToken { get; set; }
        public string stationName { get; set; }
        public string stationSharingUrl { get; set; }
        public bool requiresCleanAds { get; set; }
        public bool allowRename { get; set; }
        public bool allowAddMusic { get; set; }
        public List<string> quickMixStationIds { get; set; }
        public bool allowDelete { get; set; }
        public bool allowEditDescription { get; set; }

    }
}