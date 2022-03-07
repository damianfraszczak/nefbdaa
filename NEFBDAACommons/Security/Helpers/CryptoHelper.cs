using System;
using System.Security.Cryptography;
using System.Text;

namespace NEFBDAACommons.Helpers.Security
{
    public static class CryptoHelper
    {
        public static string CreateHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(value);
            }

            using (var algorithm = new SHA512Managed())
            {
                var bytes = algorithm.ComputeHash(Encoding.Unicode.GetBytes(value));
                return Convert.ToBase64String(bytes, 0, bytes.Length);
            }
        }
    }
}
