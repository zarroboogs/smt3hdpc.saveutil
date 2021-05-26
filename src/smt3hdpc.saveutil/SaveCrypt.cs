using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace SMT3HDSaveUtil
{
    internal class SaveCrypt
    {
        static readonly string sKey = "R045bG1lVk9qUXh1R2lSSF";
        static readonly string sIV = "TllkT2ZVaTFvWS1seGlEaE";

        static SaveCrypt()
        {
            sKey += "dpMG5fZzBuM3Fzd1dhYlk=";
            sIV += "42QUljN3ZDY2pTdl81SXI=";
            sKey = Encoding.UTF8.GetString(Convert.FromBase64String(sKey));
            sIV = Encoding.UTF8.GetString(Convert.FromBase64String(sIV));
        }

        public static byte[] Encrypt(byte[] buffer)
            => Crypt(buffer, true);

        public static byte[] Decrypt(byte[] buffer)
            => Crypt(buffer, false);

        public static byte[] Crypt(byte[] buffer, bool encrypt)
        {
            // rijndael 256 cbc pkcs7
            var engine = new RijndaelEngine(256);
            var block = new CbcBlockCipher(engine);
            var cipher = new PaddedBufferedBlockCipher(block, new Pkcs7Padding());

            var keyP = new KeyParameter(Encoding.UTF8.GetBytes(sKey));
            var keyPIV = new ParametersWithIV(keyP, Encoding.UTF8.GetBytes(sIV), 0, 32);

            cipher.Init(encrypt, keyPIV);

            var res = new byte[cipher.GetOutputSize(buffer.Length)];
            var len = cipher.ProcessBytes(buffer, res, 0);
            len += cipher.DoFinal(res, len);

            return res[..len];
        }

        public static bool IsEncrypted(byte[] data)
             => Encoding.ASCII.GetString(data[4..8]) != "VER\x07";

        public static void CryptFile(string path, string pathOut)
        {
            var data = File.ReadAllBytes(path);
            var wasEncrypted = IsEncrypted(data);

            Console.WriteLine(wasEncrypted ? "Decrypting..." : "Encrypting...");
            data = Crypt(data, !wasEncrypted);

            if (wasEncrypted && IsEncrypted(data))
            {
                Console.WriteLine("Decryption failed, the file might not be a valid SMT3HD PC save");
                return;
            }

            if (string.IsNullOrWhiteSpace(pathOut))
            {
                pathOut = wasEncrypted ? $"{path}_dec" : $"{path}_enc";
            }

            Console.WriteLine($"Writing to {pathOut}...");
            File.WriteAllBytes(pathOut, data);

            Console.WriteLine("Done");
        }
    }
}
