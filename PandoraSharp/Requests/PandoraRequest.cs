using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PandoraSharp.Requests
{
    [DataContract]
    abstract class PandoraRequest
    {
        public string Protocol { get; set; }
        private string Method;
        public bool needEncryption { get; set; } = true;
        public Type expectedResponseType;
        public abstract string getURLParameters();
            
    }
}
