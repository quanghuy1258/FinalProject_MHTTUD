using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FinalProject_MHTTUD
{
    public static class DataEncryption
    {
        public static byte[] encrypt(string receiverPublicKey, byte[] plaintext)
        {
            byte[] encryptedText;

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.GenerateKey();
            aes.GenerateIV();

            ICryptoTransform encryptor = aes.CreateEncryptor();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plaintext, 0, plaintext.Length);
                    csEncrypt.FlushFinalBlock();
                    encryptedText = msEncrypt.ToArray();
                }
            }

            byte[] encryptedKey = Account.encryptData(receiverPublicKey, aes.Key);

            byte[] ciphertext = new byte[1 + encryptedKey.Length + aes.IV.Length + encryptedText.Length];
            ciphertext[0] = (byte)encryptedKey.Length;
            Buffer.BlockCopy(encryptedKey, 0, ciphertext, 1, encryptedKey.Length);
            Buffer.BlockCopy(aes.IV, 0, ciphertext, 1 + encryptedKey.Length, aes.IV.Length);
            Buffer.BlockCopy(encryptedText, 0, ciphertext, 1 + encryptedKey.Length + aes.IV.Length, encryptedText.Length);

            return ciphertext;
        }
        public static byte[] decrypt(Account receiver, string passphrase, byte[] ciphertext)
        {
            if (receiver.checkPassphrase(passphrase))
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = Account.decryptData(receiver, passphrase, ciphertext.Skip(1).Take(ciphertext[0]).ToArray());
                aes.GenerateIV();
                aes.IV = ciphertext.Skip(1 + ciphertext[0]).Take(aes.IV.Length).ToArray();

                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(ciphertext, 1 + ciphertext[0] + aes.IV.Length, ciphertext.Length - (1 + ciphertext[0] + aes.IV.Length));
                        csDecrypt.FlushFinalBlock();
                        return msDecrypt.ToArray();
                    }
                }
            } else return null;
        }
    }
}
