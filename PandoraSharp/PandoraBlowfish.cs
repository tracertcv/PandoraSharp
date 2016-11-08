using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;

namespace PandoraSharp
{
    class PandoraBlowfish
    {
        private string EncryptKey;
        private string DecryptKey;        

        public PandoraBlowfish(string encryptKey, string decryptKey)
        {
            this.EncryptKey = encryptKey;
            this.DecryptKey = decryptKey;
        }

        //Methods for Blowfish encryption
        public string Encrypt(string data)
        {
            byte[] dataAsBytes = System.Text.Encoding.UTF8.GetBytes(data);
            byte[] keyAsBytes = System.Text.Encoding.Default.GetBytes(EncryptKey);
            int inputLength = dataAsBytes.Length;
            IBufferedCipher bf = CipherUtilities.GetCipher("Blowfish/ECB/PKCS5Padding");
            KeyParameter key = new KeyParameter(keyAsBytes);
            bf.Init(true, key);
            byte[] output = new byte[bf.GetOutputSize(inputLength)];
            int lengthout = bf.ProcessBytes(dataAsBytes, output, 0);
            bf.DoFinal(output, lengthout);
            string final = System.Text.Encoding.UTF8.GetString(Hex.Encode(output));
            return final;
        }

        public string Decrypt(string data)
        {
            byte[] dataAsBytes = Hex.Decode(data);
            byte[] keyAsBytes = System.Text.Encoding.Default.GetBytes(DecryptKey);
            int inputLength = dataAsBytes.Length;
            IBufferedCipher bf = CipherUtilities.GetCipher("Blowfish/ECB/PKCS5Padding");
            KeyParameter key = new KeyParameter(keyAsBytes);
            bf.Init(false, key);
            byte[] output = new byte[bf.GetOutputSize(inputLength)];
            int outputLength = bf.ProcessBytes(dataAsBytes, output, 0);
            bf.DoFinal(output, outputLength);
            string final = System.Text.Encoding.ASCII.GetString(output);
            return final;
        }
    }
}
