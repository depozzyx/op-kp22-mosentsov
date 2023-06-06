using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

class Program 
{
    static void Main(String[] args) 
    {
        var store = new PasswordManager.PasswordManagerStoreMemory("passwords.json");
        var auth = new PasswordManager.PasswordManagerAuth("password_unlock", "session1")
            .expiresIn(10);

        var manager = new PasswordManager(store);
        manager
            .unlock(auth)
            .save("test", "testpass")
            .lockAgain();

        manager.unlock(auth);
        Console.WriteLine(manager.retreive("test"));
        manager.lockAgain();
    }
}

class UI 
{

}

class PasswordManager
{
    // classes
    public class Password 
    {
        // classes
        public abstract class PasswordAlghorithm {
            public abstract byte[] encrypt(string value, string encryptionKey);
            public abstract byte[] encrypt(string value, byte[] encryptionKey);
            public abstract string decrypt(byte[] value, string encryptionKey);
            public abstract string decrypt(byte[] value, byte[] encryptionKey);
        }

        public class PasswordAlghorithmAES : PasswordAlghorithm 
        {
            public Aes getAes(byte[] passwordBytes)
            {
                var aes = Aes.Create();
                byte[] aesKey = SHA256Managed.Create().ComputeHash(passwordBytes);
                byte[] aesIV = MD5.Create().ComputeHash(passwordBytes);
                aes.Key = aesKey;
                aes.IV = aesIV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                
                return aes;
            }
            
            public override byte[] encrypt(string value, string encryptionKey)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionKey);
                return encrypt(value, passwordBytes);
            }
            public override byte[] encrypt(string value, byte[] encryptionKey)
            {
                byte[] encrypted;
                using (Aes aesAlg = getAes(encryptionKey))
                {
                    // Create an encryptor to perform the stream transform.
                    var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(value);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                return encrypted;
            }
            public override string decrypt(byte[] value, string encryptionKey)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionKey);
                return decrypt(value, passwordBytes);
            }
            public override string decrypt(byte[] value, byte[] encryptionKey)
            {
                string plaintext = null;

                using (Aes aesAlg = getAes(encryptionKey))
                {
                    // Create a decryptor to perform the stream transform.
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(value))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                return plaintext;
            }
        }
        public class PasswordAlghorithmPlainText : PasswordAlghorithm 
        {
            public override byte[] encrypt(string value, string encryptionKey)
            {
                return Encoding.UTF8.GetBytes(value);
            }
            public override byte[] encrypt(string value, byte[] encryptionKey)
            {
                return Encoding.UTF8.GetBytes(value);
            }
            public override string decrypt(byte[] value, string encryptionKey)
            {
                return Encoding.UTF8.GetString(value);
            }
            public override string decrypt(byte[] value, byte[] encryptionKey)
            {
                return Encoding.UTF8.GetString(value);
            }
        }

        // vars
        public PasswordAlghorithm alghorithm {get; }
        public byte[] value {get; }

        // methods
        public Password(PasswordAlghorithm alghorithm, string password, string encryptionKey) 
        {
            this.alghorithm = alghorithm;
            this.value = alghorithm.encrypt(password, encryptionKey);
        }
        public Password(PasswordAlghorithm alghorithm, string password, byte[] encryptionKey) 
        {
            this.alghorithm = alghorithm;
            this.value = alghorithm.encrypt(password, encryptionKey);
        }

        public string decrypt(string encryptionKey) 
        {
            var value = alghorithm.decrypt(this.value, encryptionKey);
            return value;
        }
    }

    public class PasswordManagerAuth 
    {
        // classes

        // vars
        private Password password;
        public uint expiresOn;
        public string sessionId;

        // methods
        public PasswordManagerAuth(string password, string sessionId) 
        {
            var pwd = new Password(new Password.PasswordAlghorithmPlainText(), password, sessionId);
            this.password = pwd;
            this.sessionId = sessionId;
        }
        public PasswordManagerAuth expiresIn(uint seconds)
        {
            this.expiresOn = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + seconds;
            return this;
        }
        public Password getPassword()
        {
            if ((uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds > this.expiresOn)
                throw new InvalidOperationException("password is expired, log in again");
            
            return password;
        }
    }

    public abstract class PasswordManagerStore 
    {
        // classes

        // vars

        // methods
        public abstract void save(string key, Password password);
        public abstract Password get(string key);
    }
    public class PasswordManagerStoreMemory : PasswordManagerStore
    {
        // classes

        // vars
        public string path;
        private IDictionary<string, Password> passwords;

        // methods
        public PasswordManagerStoreMemory(string path)
        {
            this.path = path;
            this.passwords = new Dictionary<string, Password>();   
        }

        public override void save(string key, Password password)
        {
            passwords[key] = password;
        }

        public override Password get(string key)
        {
            return passwords[key];
        }
    }
    
    // vars
    PasswordManagerStore store;
    PasswordManagerAuth auth;

    // methods
    public PasswordManager(PasswordManagerStore store)
    {
        this.store = store;
    }

    public PasswordManager unlock(PasswordManagerAuth auth)
    {
        this.auth = auth;
        return this;
    }

    public PasswordManager lockAgain()
    {
        this.auth = null;
        return this;
    }

    public PasswordManager save(string key, string password)
    {
        if (this.auth == null) throw new ArgumentNullException("auth is null");

        var pwd = new Password(new Password.PasswordAlghorithmAES(), password, auth.getPassword().decrypt(auth.sessionId));
        this.store.save(key, pwd);

        return this;
    }

    public string retreive(string key)
    {
        if (this.auth == null) throw new ArgumentNullException("auth is null");

        Password pwd = this.store.get(key);
        return pwd.decrypt(auth.getPassword().decrypt(auth.sessionId));
    }
}

class TestUI 
{

}

class TestPasswordManager 
{

}