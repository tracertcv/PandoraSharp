using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Structures
{
    public class PSong
    {

        protected const string LowQuality = "lowQuality";
        protected const string MediumQuality = "mediumQuality";
        protected const string HighQuality = "highQuality";

        public string trackToken { get; set; }
        public string trackGain { get; set; }

        public string songName { get; set; }
        public string songExplorerUrl { get; set; }
        public string songDetailUrl { get; set; }
        public int songRating { get; set; }

        public string artistName { get; set; }
        public string artistExplorerUrl { get; set; }
        public string artistDetailUrl { get; set; }

        public string albumName { get; set; }
        public string albumArtUrl { get; set; }
        public string albumDetailUrl { get; set; }
        public string albumExplorerUrl { get; set; }

        public string amazonAlbumUrl { get; set; }
        public string amazonAlbumAsin { get; set; }
        public string amazonAlbumDigitalAsin { get; set; }
        public string amazonSongDigitalAsin { get; set; }

        public string stationId { get; set; }
        public string nowPlayingSationAdUrl { get; set; }

        public string itunesSongUrl { get; set; }

        public bool allowFeedback { get; set; }

        public Dictionary <string, MusicRetrievalLink> audioUrlMap { get; set; }
        public List<String> additionalAudioUrl { get; set; }

        public string adToken { get; set; }

        public bool isAd()
        {
            return adToken == null;
        }

        public string FileName { get; set; }

        public FileStream getMP3(string quality)
        {
            string fileName = "tmp." + this.songName + ".mp3";
            FileName = fileName;
            WebClient myClient = new WebClient();
            myClient.DownloadFile(audioUrlMap[quality].audioUrl, fileName);
            return new FileStream(fileName, FileMode.Open);
        }
    }
}
