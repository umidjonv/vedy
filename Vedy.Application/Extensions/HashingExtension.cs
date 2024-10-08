﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vedy.Application.Extensions
{
    public static class HashingExtension
    {
        const int keySize = 64;
        const int iterations = 350000;
        const string Salt = "FocusOnYourPurposeItWillBeGreat";
        public static string HashPasword(string password)
        {
            

            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var salt = Encoding.ASCII.GetBytes(HashingExtension.Salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }
        public static bool VerifyPassword(string password, string hash)
        {
            var salt = Encoding.ASCII.GetBytes(HashingExtension.Salt);
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
