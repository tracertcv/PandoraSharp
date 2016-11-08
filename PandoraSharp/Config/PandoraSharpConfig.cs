using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PandoraSharp.Config
{
        class PandoraSharpConfig
        {
            public PandoraSharpConfig() { }
            public PandoraConnectorConfig ConnectorConfig { get; set; }
            public PandoraClientConfig ClientConfig { get; set; }
        }
}
