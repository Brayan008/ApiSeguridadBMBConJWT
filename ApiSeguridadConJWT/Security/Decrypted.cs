using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ApiSeguridadConJWT.Security
{
    public class Decrypted
    {
        public static string DesencriptarBase64(string sBase64)
        {
            string encripteData = string.Empty;
            if (!string.IsNullOrEmpty(sBase64))
            {
                byte[] byteArray = Convert.FromBase64String(sBase64);
                encripteData = Encoding.UTF8.GetString(byteArray);
            }
            return encripteData;
        }
    }
}