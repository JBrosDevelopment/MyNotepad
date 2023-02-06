using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyNotepad
{
    public static class Encrypt
    {
        public static string encrypt(string ServerName)
        {
            byte[] NameEncodein = new byte[ServerName.Length];
            NameEncodein = System.Text.Encoding.UTF8.GetBytes(ServerName);
            string EcodedName = Convert.ToBase64String(NameEncodein);
            return EcodedName;
        }

        public static string Decrypt(string Servername)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder strDecoder = encoder.GetDecoder();
            byte[] to_DecodeByte = Convert.FromBase64String(Servername);
            int charCount = strDecoder.GetCharCount(to_DecodeByte, 0, to_DecodeByte.Length);
            char[] decoded_char = new char[charCount];
            strDecoder.GetChars(to_DecodeByte, 0, to_DecodeByte.Length, decoded_char, 0);
            string Name = new string(decoded_char);

            return Name;
        }
    }

}
