using System;
using System.Text.RegularExpressions;

namespace Medihelp.Graph.Core.Utils.Extensions
{
    public static class StringValidationExtension
    {
        private static Regex regBase64 = new Regex(@"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.Compiled);

        public static Boolean IsBase64String(this String s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && regBase64.IsMatch(s);
        }


        public static Boolean IsBase128String(this String s, out string decodedBase64)
        {
            byte[] data = System.Convert.FromBase64String(s);

            decodedBase64 = System.Text.ASCIIEncoding.ASCII.GetString(data);

            return decodedBase64.IsBase64String();
        }

    }
}
