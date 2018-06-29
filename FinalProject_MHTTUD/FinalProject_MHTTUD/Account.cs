using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FinalProject_MHTTUD
{
    public class Account
    {
        private string _emailAddress;
        private string _fullName;
        private string _birthday;
        private string _phoneNumber;
        private string _address;
        private string _passphraseWithSalt;
        private string _publicKey;
        private string _encryptedPrivateKey;

        public string email
        {
            get
            {
                return this._emailAddress;
            }
        }

        private static byte[] encryptWithPassphrase(string passphrase, string plaintext)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashPassphrase = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Key = hashPassphrase;
            aes.GenerateIV();

            ICryptoTransform encryptor = aes.CreateEncryptor();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plaintext);
                    }
                    byte[] encryptedText = msEncrypt.ToArray();
                    byte[] res = new byte[aes.IV.Length + encryptedText.Length];
                    Buffer.BlockCopy(aes.IV, 0, res, 0, aes.IV.Length);
                    Buffer.BlockCopy(encryptedText, 0, res, aes.IV.Length, encryptedText.Length);
                    return res;
                }
            }
        }
        private static string decryptWithPassphrase(string passphrase, byte[] ciphertext)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashPassphrase = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Key = hashPassphrase;
            aes.GenerateIV();
            aes.IV = ciphertext.Take(aes.IV.Length).ToArray();

            ICryptoTransform decryptor = aes.CreateDecryptor();
            try
            {
                using (MemoryStream msDecrypt = new MemoryStream(ciphertext.Skip(aes.IV.Length).ToArray()))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public Account()
        {
            this._emailAddress = null;
            this._fullName = "";
            this._birthday = "";
            this._phoneNumber = "";
            this._address = "";
            this._passphraseWithSalt = "";
            this._publicKey = "";
            this._encryptedPrivateKey = "";
        }
        public Account inputInfo(string emailAddress, string fullName, string birthday, string phoneNumber, string address)
        {
            this._emailAddress = emailAddress;
            this._fullName = fullName;
            this._birthday = birthday;
            this._phoneNumber = phoneNumber;
            this._address = address;
            return this;
        }
        public Account generateKey(string passphrase, int nbits)
        {
            byte[] salt = new byte[256 / 8];
            Random random = new Random(Guid.NewGuid().GetHashCode());
            random.NextBytes(salt);

            SHA256 sha256 = SHA256.Create();
            byte[] hashPassphrase = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            
            byte[] hashPassphraseWithSalt = new byte[(256 + 256) / 8];
            Buffer.BlockCopy(hashPassphrase, 0, hashPassphraseWithSalt, 0, 256 / 8);
            Buffer.BlockCopy(salt, 0, hashPassphraseWithSalt, 256 / 8, 256 / 8);
            Buffer.BlockCopy(sha256.ComputeHash(hashPassphraseWithSalt), 0, hashPassphraseWithSalt, 0, 256 / 8);

            this._passphraseWithSalt = Convert.ToBase64String(hashPassphraseWithSalt);

            if (nbits < 512) nbits = 512;
            else if (nbits > 1024) nbits = 1024;
            else nbits = (nbits / 64) * 64;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(nbits);

            this._encryptedPrivateKey = Convert.ToBase64String(encryptWithPassphrase(passphrase, rsa.ToXmlString(true)));
            this._publicKey = rsa.ToXmlString(false);

            return this;
        }

        public bool checkPassphrase(string passphrase)
        {
            if (this._passphraseWithSalt == "") return false;
            SHA256 sha256 = SHA256.Create();
            byte[] hashPassphrase = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));

            byte[] hashPassphraseWithSalt = new byte[(256 + 256) / 8];
            Buffer.BlockCopy(hashPassphrase, 0, hashPassphraseWithSalt, 0, 256 / 8);
            Buffer.BlockCopy(Convert.FromBase64String(this._passphraseWithSalt), 256/8, hashPassphraseWithSalt, 256 / 8, 256 / 8);
            Buffer.BlockCopy(sha256.ComputeHash(hashPassphraseWithSalt), 0, hashPassphraseWithSalt, 0, 256 / 8);

            if (this._passphraseWithSalt == Convert.ToBase64String(hashPassphraseWithSalt)) return true;
            else return false;
        }
        public Account changeFullName(string passphrase, string fullName)
        {
            if (this.checkPassphrase(passphrase))
                this._fullName = fullName;
            return this;
        }
        public Account changeBirthday(string passphrase, string birthday)
        {
            if (this.checkPassphrase(passphrase))
                this._birthday = birthday;
            return this;
        }
        public Account changePhoneNumber(string passphrase, string phoneNumber)
        {
            if (this.checkPassphrase(passphrase))
                this._phoneNumber = phoneNumber;
            return this;
        }
        public Account changeAddress(string passphrase, string address)
        {
            if (this.checkPassphrase(passphrase))
                this._address = address;
            return this;
        }
        public Account changePassphrase(string oldPassphrase, string newPassphrase)
        {
            if (this.checkPassphrase(oldPassphrase))
            {
                byte[] salt = new byte[256 / 8];
                Random random = new Random(Guid.NewGuid().GetHashCode());
                random.NextBytes(salt);

                SHA256 sha256 = SHA256.Create();
                byte[] hashPassphrase = sha256.ComputeHash(Encoding.UTF8.GetBytes(newPassphrase));

                byte[] hashPassphraseWithSalt = new byte[(256 + 256) / 8];
                Buffer.BlockCopy(hashPassphrase, 0, hashPassphraseWithSalt, 0, 256 / 8);
                Buffer.BlockCopy(salt, 0, hashPassphraseWithSalt, 256 / 8, 256 / 8);
                Buffer.BlockCopy(sha256.ComputeHash(hashPassphraseWithSalt), 0, hashPassphraseWithSalt, 0, 256 / 8);

                this._passphraseWithSalt = Convert.ToBase64String(hashPassphraseWithSalt);

                this._encryptedPrivateKey = Convert.ToBase64String(encryptWithPassphrase(newPassphrase, decryptWithPassphrase(oldPassphrase, Convert.FromBase64String(this._encryptedPrivateKey))));
            }
            return this;
        }

        public string export()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode accountNode = doc.CreateElement("account");
            doc.AppendChild(accountNode);

            XmlNode emailNode = doc.CreateElement("email");
            emailNode.AppendChild(doc.CreateTextNode(this._emailAddress));
            accountNode.AppendChild(emailNode);

            XmlNode fullNameNode = doc.CreateElement("full_name");
            fullNameNode.AppendChild(doc.CreateTextNode(this._fullName));
            accountNode.AppendChild(fullNameNode);

            XmlNode birthdayNode = doc.CreateElement("birthday");
            birthdayNode.AppendChild(doc.CreateTextNode(this._birthday));
            accountNode.AppendChild(birthdayNode);

            XmlNode phoneNumberNode = doc.CreateElement("phone_number");
            phoneNumberNode.AppendChild(doc.CreateTextNode(this._phoneNumber));
            accountNode.AppendChild(phoneNumberNode);

            XmlNode addressNode = doc.CreateElement("address");
            addressNode.AppendChild(doc.CreateTextNode(this._address));
            accountNode.AppendChild(addressNode);

            XmlNode passphraseNode = doc.CreateElement("passphrase_with_salt");
            passphraseNode.AppendChild(doc.CreateTextNode(this._passphraseWithSalt));
            accountNode.AppendChild(passphraseNode);

            XmlNode publicKeyNode = doc.CreateElement("public_key");
            publicKeyNode.AppendChild(doc.CreateTextNode(this._publicKey));
            accountNode.AppendChild(publicKeyNode);

            XmlNode privateKeyNode = doc.CreateElement("encrypted_private_key");
            privateKeyNode.AppendChild(doc.CreateTextNode(this._encryptedPrivateKey));
            accountNode.AppendChild(privateKeyNode);
            
            return doc.OuterXml;
        }
        public bool import(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList accountNodeList = doc.GetElementsByTagName("account");
            if (accountNodeList.Count != 1) return false;
            XmlNode accountNode = accountNodeList[0];

            try
            {
                this._emailAddress = accountNode.SelectSingleNode("email").InnerText;
                this._fullName = accountNode.SelectSingleNode("full_name").InnerText;
                this._birthday = accountNode.SelectSingleNode("birthday").InnerText;
                this._phoneNumber = accountNode.SelectSingleNode("phone_number").InnerText;
                this._address = accountNode.SelectSingleNode("address").InnerText;
                this._passphraseWithSalt = accountNode.SelectSingleNode("passphrase_with_salt").InnerText;
                this._publicKey = accountNode.SelectSingleNode("public_key").InnerText;
                this._encryptedPrivateKey = accountNode.SelectSingleNode("encrypted_private_key").InnerText;
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public string getEmailAddress()
        {
            return this._emailAddress;
        }
        public string getFullName()
        {
            return this._fullName;
        }
        public string getBirthday()
        {
            return this._birthday;
        }
        public string getPhoneNumber()
        {
            return this._phoneNumber;
        }
        public string getAddress()
        {
            return this._address;
        }
        public string getPublicKey()
        {
            return this._publicKey;
        }
        public static byte[] encryptData(string publicKey, byte[] plaintext)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publicKey);
                return rsa.Encrypt(plaintext, false);
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static byte[] decryptData(Account owner, string passphrase, byte[] ciphertext)
        {
            if (owner.checkPassphrase(passphrase))
            {
                try
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(decryptWithPassphrase(passphrase, Convert.FromBase64String(owner._encryptedPrivateKey)));
                    return rsa.Decrypt(ciphertext, false);
                } catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else return null;            
        }
        public static byte[] signData(Account owner, string passphrase, byte[] data)
        {
            if (owner.checkPassphrase(passphrase))
            {
                try
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(decryptWithPassphrase(passphrase, Convert.FromBase64String(owner._encryptedPrivateKey)));
                    return rsa.SignData(data, new SHA256CryptoServiceProvider());
                } catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else return null;
        }
        public static bool verifyData(string ownerPublicKey, byte[] data, byte[] signature)
        {
            if (data == null || signature == null) return false;
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(ownerPublicKey);
                return rsa.VerifyData(data, new SHA256CryptoServiceProvider(), signature);
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static string exportDatabase(List<Account> db)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode accountsNode = doc.CreateElement("accounts");
            doc.AppendChild(accountsNode);

            for (int i = 0; i < db.Count; i++)
            {
                XmlNode accountNode = doc.CreateElement("account");
                accountsNode.AppendChild(accountNode);

                XmlNode emailNode = doc.CreateElement("email");
                emailNode.AppendChild(doc.CreateTextNode(db[i]._emailAddress));
                accountNode.AppendChild(emailNode);

                XmlNode fullNameNode = doc.CreateElement("full_name");
                fullNameNode.AppendChild(doc.CreateTextNode(db[i]._fullName));
                accountNode.AppendChild(fullNameNode);

                XmlNode birthdayNode = doc.CreateElement("birthday");
                birthdayNode.AppendChild(doc.CreateTextNode(db[i]._birthday));
                accountNode.AppendChild(birthdayNode);

                XmlNode phoneNumberNode = doc.CreateElement("phone_number");
                phoneNumberNode.AppendChild(doc.CreateTextNode(db[i]._phoneNumber));
                accountNode.AppendChild(phoneNumberNode);

                XmlNode addressNode = doc.CreateElement("address");
                addressNode.AppendChild(doc.CreateTextNode(db[i]._address));
                accountNode.AppendChild(addressNode);

                XmlNode passphraseNode = doc.CreateElement("passphrase_with_salt");
                passphraseNode.AppendChild(doc.CreateTextNode(db[i]._passphraseWithSalt));
                accountNode.AppendChild(passphraseNode);

                XmlNode publicKeyNode = doc.CreateElement("public_key");
                publicKeyNode.AppendChild(doc.CreateTextNode(db[i]._publicKey));
                accountNode.AppendChild(publicKeyNode);

                XmlNode privateKeyNode = doc.CreateElement("encrypted_private_key");
                privateKeyNode.AppendChild(doc.CreateTextNode(db[i]._encryptedPrivateKey));
                accountNode.AppendChild(privateKeyNode);
            }
            return doc.OuterXml;
        }
        public static bool importDatabase(string xml, ref List<Account> db)
        {
            List<Account> newDB = new List<Account>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList accountNodeList = doc.GetElementsByTagName("account");
            if (accountNodeList.Count < 1) return false;
            
            for (int i = 0; i < accountNodeList.Count; i++)
            {
                try
                {
                    Account acc = new Account();
                    acc._emailAddress = accountNodeList[i].SelectSingleNode("email").InnerText;
                    acc._fullName = accountNodeList[i].SelectSingleNode("full_name").InnerText;
                    acc._birthday = accountNodeList[i].SelectSingleNode("birthday").InnerText;
                    acc._phoneNumber = accountNodeList[i].SelectSingleNode("phone_number").InnerText;
                    acc._address = accountNodeList[i].SelectSingleNode("address").InnerText;
                    acc._passphraseWithSalt = accountNodeList[i].SelectSingleNode("passphrase_with_salt").InnerText;
                    acc._publicKey = accountNodeList[i].SelectSingleNode("public_key").InnerText;
                    acc._encryptedPrivateKey = accountNodeList[i].SelectSingleNode("encrypted_private_key").InnerText;
                    newDB.Add(acc);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            db = newDB;
            return true;
        }
    }
}
