﻿using System;
using System.Security.Cryptography;
using System.Text;
using SportBet.Services.Contracts.Encryption;

namespace SportBet.Services.Encryption
{
    public class Encryptor : IEncryptor
    {
        private const string Salt = "AGSF12ryb1*&T8&GTg8T";

        public string Encrypt(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Encoding.Unicode.GetBytes(Salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }
    }
}
