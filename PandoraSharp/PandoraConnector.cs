using System.Net;
using System.IO;
using System;
using System.Text;
using System.Runtime.Serialization.Json;
using PandoraSharp.Requests;
using PandoraSharp.Config;
using Newtonsoft.Json;
using PandoraSharp.Responses;
using PandoraSharp.Exceptions;

namespace PandoraSharp
{
    internal class PandoraConnector
    {
        //Low level config options
        private PandoraConnectorConfig Config;

        //Encryption
        PandoraBlowfish PBlowfish;

        public PandoraConnector (PandoraConnectorConfig config, PandoraBlowfish encryptor)
        {
            this.Config = config;
            this.PBlowfish = encryptor;
        }


        public PandoraResponse doPost(PandoraRequest data)
        {
            try
            {
                //Build URL
                string url = data.Protocol + Config.BaseURL + data.getURLParameters();
                Type expectedResponseType = data.expectedResponseType;
                //Convert data to raw bytes so we can stream it to the request, encrypting as necessary
                byte[] databuffer = serializeRequestObject(data);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                //Set the request headers
                request.Method = Config.RequestMethod;
                request.UserAgent = Config.RequestUserAgent;
                request.ContentType = Config.RequestContentType;
                request.Accept = Config.RequestAccept;
                request.ContentLength = databuffer.Length;

                //Get the request stream and send out data
                Stream stream = request.GetRequestStream();
                stream.Write(databuffer, 0, databuffer.Length);
                stream.Close();

                //Get the response
                WebResponse resp = request.GetResponse();
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                string rawResponse = reader.ReadToEnd();
                PandoraResponse response = JsonConvert.DeserializeObject<PandoraResponse>(rawResponse);

                //Debug
                //Console.WriteLine(url);
                //Console.WriteLine(rawResponse);

                if (response.stat == "ok")
                {
                    IPandoraResponseResult resultObj = (IPandoraResponseResult)Activator.CreateInstance(expectedResponseType);
                    resultObj.populate(Convert.ToString(response.result));
                    response.result = resultObj;
                }
                return response;
            }catch(PandoraRequestURLFormatException eURL)
            {
                log("Couldn't do post -");
                log("Malformed URL.");
                return null;
            }
        }

        private byte[] serializeRequestObject(PandoraRequest request)
        {
            //Set up serializer
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(request.GetType());
            StreamReader reader = new StreamReader(stream);

            //Serialize the object
            serializer.WriteObject(stream, request);

            //Read the object from the stream and convert to byte array
            stream.Position = 0;
            string result = reader.ReadToEnd();
            if(request.needEncryption)
            {
                result = PBlowfish.Encrypt(result);
            }
            return Encoding.ASCII.GetBytes(result);
        }

        private void log(string message)
        {
            Console.WriteLine(message);
        }
    }
}