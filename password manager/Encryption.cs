using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace password_manager
{
    internal static class EncryptionUtilities

                    
    {
        private static readonly string _OriginalInfo= "ABCDEFGHIJKLMNOPQRSTUVWXYZab.cdefghijklmnop,qrstuvwxyz012:3456789!@#$%^&*()_+-={}[]";
        private static readonly string _alternativeInfo = "hijklmnopqrstuvwxyz012-={}[]ABC345,6789!@#$%^&*()_+DEFGHIJ.KLMNOPQRS:TUVWXYZabcdefg";
        public static string encrypt(string info)
        {
            var sb = new StringBuilder();
            foreach (char c in info)
            {
                var charindex = _OriginalInfo.IndexOf(c);
                sb.Append(_alternativeInfo[charindex]);
            }
            return sb.ToString();
        }
        public static string decrypt(string info)
        {
            var sb = new StringBuilder();
            foreach (char c in info)
            {
                var charindex = _alternativeInfo.IndexOf(c);
                sb.Append(_OriginalInfo[charindex]);
            }
            return sb.ToString();
        }
        public static string RemoveWhiteSpaces(this string value)
        {
            return value.Replace(" ", "");
        }
    }
}
