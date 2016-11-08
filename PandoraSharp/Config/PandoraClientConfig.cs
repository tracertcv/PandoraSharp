using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PandoraSharp.Config
{
    [DataContract]
    class PandoraClientConfig
    {
        //Partner auth data
        [DataMember]
        public string ParterUser { get; set; } = "android";
        [DataMember]
        public string PartnerPassword { get; set; } = "AC7IBG09A3DTSYM4R41UJWL07VLN8JI7";
        [DataMember]
        public string DeviceModel { get; set; } = "android-generic";
        [DataMember]
        public string Version { get; set; } = "5";

        //User credentials
        [DataMember]
        public string Username { get; set; } = "";
        [DataMember]
        public string UserPass { get; set; } = "";

        //Encrypt/Decrypt keys for Blowfish cipher
        [DataMember]
        public string EncryptKey { get; set; } = "6#26FRL$ZWD";
        [DataMember]
        public string DecryptKey { get; set; } = "R=U!LH$O2B#";

        public PandoraClientConfig() { }
    }
}
