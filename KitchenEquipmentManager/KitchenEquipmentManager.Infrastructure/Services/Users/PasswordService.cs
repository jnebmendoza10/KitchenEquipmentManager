using System;
using System.Security.Cryptography;

namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    /// <summary>
    /// Copyright: The hashing algorithm logic is retrieved from:https://gist.github.com/hclewk/a41d937eba12c0388f70429a997cd7ec
    /// </summary>
    public class PasswordService : IPasswordService
    {
        // The following constants may be changed without breaking existing hashes.
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        public string CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = hashedPassword.Split(delimiter);
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
