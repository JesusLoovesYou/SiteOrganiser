using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SiteOrganiser.DataAccess.Helpers
{
    public static class Encryption
    {
        private const string EncriptionKey = "MVPTemplate2014";

        public static string Encrypt(string strQueryStringParameter)
        {
            var hashFunc = new MD5CryptoServiceProvider();
            var key = hashFunc.ComputeHash(Encoding.ASCII.GetBytes(EncriptionKey));
            var iv = new byte[8];
            var shaFunc = new SHA1CryptoServiceProvider();
            var temp = shaFunc.ComputeHash(Encoding.ASCII.GetBytes(EncriptionKey));
            for (var i = 0; i < 8; i++)
                iv[i] = temp[i];
            var toenc = Encoding.UTF8.GetBytes(strQueryStringParameter);
            var des = new TripleDESCryptoServiceProvider { KeySize = 128 };

            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            cs.Write(toenc, 0, toenc.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string strQueryStringParameter)
        {
            var hashFunc = new MD5CryptoServiceProvider();
            var key = hashFunc.ComputeHash(Encoding.ASCII.GetBytes(EncriptionKey));
            var iv = new byte[8];
            var shaFunc = new SHA1CryptoServiceProvider();
            var temp = shaFunc.ComputeHash(Encoding.ASCII.GetBytes(EncriptionKey));
            for (var i = 0; i < 8; i++)
                iv[i] = temp[i];
            var todec = Convert.FromBase64String(strQueryStringParameter);
            var des = new TripleDESCryptoServiceProvider { KeySize = 128 };
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Write);
            cs.Write(todec, 0, todec.Length);
            cs.FlushFinalBlock();
            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
