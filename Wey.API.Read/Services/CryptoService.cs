using System;
using System.Security.Cryptography;
using System.Text;
using Wey.Common;
using Wey.Common.Exceptions;

namespace Wey.API.Read.Services
{
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <returns></returns>
        public string GenerateSalt()
        {
            var data = new byte[0x10];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public string EncryptPassword(string password, string salt)
        {
            Throw.IfNullOrEmpty<ArgumentNullOrEmptyException>("salt", salt);

            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);

                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);

                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
    }
}
