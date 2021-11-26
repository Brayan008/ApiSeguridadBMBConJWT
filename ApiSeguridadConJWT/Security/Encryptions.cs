using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ApiSeguridadConJWT.Security
{
    public class Encryptions
    {
        //Encriptar contrasena con Base64
        public static string EncriptarBase64(string dato)
        {
            string encripteData = string.Empty;
            if (!string.IsNullOrEmpty(dato))
            {
                var byteArray = Encoding.UTF8.GetBytes(dato);
                encripteData = Convert.ToBase64String(byteArray);
            }
            return encripteData;
        }

        //Encriptar con SH256
        public static string SHA256Encriptar(string data)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(data));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }


        //Encriptar con MD5
        public static string MD5Encrypt(string data)
        {
            string encripted = string.Empty;
            using (MD5 md5Handler = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                var hash = md5Handler.ComputeHash(bytes);
                StringBuilder strBuilder = new StringBuilder();
                foreach (byte b in hash)
                {
                    strBuilder.Append(b.ToString("x2"));

                }
                encripted = strBuilder.ToString();
            }
            return encripted;
        }
    }
}