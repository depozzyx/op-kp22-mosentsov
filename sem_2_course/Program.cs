using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

class Program 
{
    static void Main(String[] args) 
    {
        new PasswordManagerUI();
    }
}

public class UI 
{
    // classes
    public class UIResponse
    {
        public bool switchToNextUI;
        public UI nextUI;

        public UIResponse()
        {
            switchToNextUI = false;
        }

        public UIResponse(bool switchToNextUI, UI nextUI)
        {
            this.switchToNextUI = switchToNextUI;
            this.nextUI = nextUI;
        }
    }
    public abstract class UIComponent 
    {
        abstract public UIResponse onKey(ConsoleKey key);
        abstract public void show();
    }
    public class UITextBox : UIComponent
    {
        // vars
        string text;

        // methods
        public UITextBox(string text)
        {
            this.text = text;
        }

        public override UIResponse onKey(ConsoleKey key)
        {
            return new UIResponse();
        }

        public override void show()
        {
            Console.WriteLine("*");
            foreach (var item in this.text.Split("\n"))
            {
                Console.WriteLine("| " + item);
                
            }
            Console.WriteLine("*");
        }
    }
    public class UIInput : UIComponent
    {
        // vars
        string title;
        Func<string, string> callback;
        string inputtedText;

        // methods
        public UIInput(string title, Func<string, string> callback)
        {
            this.title = title;
            this.callback = callback;
            this.inputtedText = "";
        }

        public override UIResponse onKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Backspace) 
            {
                if (inputtedText != "") inputtedText = inputtedText.Substring(0, inputtedText.Length - 1);
            }
            else if ((int) key >= 65 && (int) key <= 90)
            {
                inputtedText += (char) (int) key;
            }

            this.callback(inputtedText);

            return new UIResponse();
        }

        public override void show()
        {
            Console.WriteLine("* " + title);
            Console.WriteLine("| [" + inputtedText + "_]");
            Console.WriteLine("*");
        }
    }
    public class UISelect : UIComponent
    {
        // classes
        class UISelectItem
        {
            public string title;
            public Func<UIResponse> action;

            public UISelectItem(string title, Func<UIResponse> action)
            {
                this.title = title;
                this.action = action;
            }
        }

        // vars
        List<UISelectItem> items;
        public int index;
        public string title;

        // methods
        public UISelect(string title)
        {
            this.title = title == null ? "Select any item" : title;
            this.index = 0;
            this.items = new List<UISelectItem>();
        }

        public override void show()
        {
            Console.WriteLine("* " + this.title);
            for (int i = 0; i < items.Count; i++)
            {
                if (index == i) Console.Write("| > ");
                else Console.Write("|   ");

                Console.Write((i + 1) + ". " + items[i].title + "\n");
            }
            Console.WriteLine("|\n* index: " + this.index);
        }

        public override UIResponse onKey(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow) index -= 1;
            else if (key == ConsoleKey.DownArrow) index += 1;
            else if (key == ConsoleKey.Enter) 
            {
                return items[index].action();
            }

            if (index >= items.Count) index = 0;
            else if(index <= -1) index = items.Count - 1;

            return new UIResponse();
        }

        public UISelect add(string title, Func<UIResponse> action)
        {
            this.items.Add(new UISelectItem(title, action));
            return this;
        }
    }

    // vars
    List<UIComponent> components;

    // methods
    public UI()
    {
        Console.Clear();
        this.components = new List<UIComponent>();
    }

    public UI add(UIComponent component)
    {
        components.Add(component);
        return this;
    }

    public UI add(UIComponent component, bool condition)
    {
        if (condition)
            components.Add(component);
        return this;
    }

    public UIResponse show()
    {
        Console.Clear();
        Console.WriteLine("* Press ESC to stop\n");

        foreach (var component in this.components)
        {
            component.show();
            Console.WriteLine("");
        }

        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Escape)
        {
            return new UIResponse(true, null);
        }

        foreach (var component in this.components)
        {
            var resp = component.onKey(key);
            if (resp.switchToNextUI) {
                return resp;
            }
        }

        return new UIResponse();
    }

    public static void showLoop(UI ui)
    {
        var show = ui.show();
        while (ui != null) {
            if (show.switchToNextUI) 
            {
                ui = show.nextUI;
            } 

            if (ui != null)
            {
                show = ui.show();
            }
        }
    }

}

public class PasswordManagerUI 
{
    // classes

    // vars
    PasswordManager.PasswordManagerStore store;
    PasswordManager manager;

    string inputtedText;
    string inputtedKey;

    // method
    public PasswordManagerUI()
    {
        var ui = mainMenu(null);
        UI.showLoop(ui);
    }

    public UI mainMenu(string notification)
    {
        return new UI()
            .add(
                new UI.UITextBox(
                    "Курсова робота Мосенцова Івана КП-22\n" +
                    "Password manager\n\n" +
                    "Use arrows and enter key to control interace"
                )
            )
            .add(
                new UI.UITextBox(notification),
                notification != null
            )
            .add(
                new UI.UISelect("Choose option")
                    .add("Unlock password manager", () => {
                        if (this.manager == null)
                            return new UI.UIResponse(true, mainMenu(
                                "No password manager store currently loaded!\n" + 
                                "Create new or load one"
                            ));
                        
                        return new UI.UIResponse(true, unlockManager());
                        
                    })
                    .add("Create memory password store", () => new UI.UIResponse(true, createStore("memory")))
                    .add("Load JSON password store", () => {

                        return new UI.UIResponse(true, createStore("memory"));
                    })
                    .add("Create JSON password store", () => new UI.UIResponse(true, createStore("json")))
                    .add("Run tests", () => {
                        PasswordManager.main();
                        return new UI.UIResponse(true, null);
                     })
                    .add("Exit", () => new UI.UIResponse(true, null))
            );
    }

    public UI createStore(string storeType)
    {
        return new UI()
            .add(
                new UI.UITextBox("Creating new store\ntype: " + storeType)
            )
            .add(
                new UI.UIInput("Enter your master password for manager", (string s) => {
                    Console.WriteLine(s);
                    this.inputtedText = s;
                    return s;
                })
            )
            .add(
                new UI.UISelect("Enter password and choose submit. Or cancel action")
                    .add("Submit", () => {
                        if (storeType == "memory")
                        {
                            this.store = new PasswordManager.PasswordManagerStoreMemory("passwords.json");
                            var auth = new PasswordManager.PasswordManagerAuth(this.inputtedText, "session1")
                                .expiresIn(600);

                            this.manager = new PasswordManager(store);
                            this.manager.setMasterPassword(auth.getPassword());
                        }
                        else if (storeType == "json")
                        {
                            this.store = new PasswordManager.PasswordManagerStoreMemory("passwords.json");
                            var auth = new PasswordManager.PasswordManagerAuth(this.inputtedText, "session1")
                                .expiresIn(600);

                            this.manager = new PasswordManager(store);
                            this.manager.setMasterPassword(auth.getPassword());
                        }
                        else 
                            return new UI.UIResponse(true, mainMenu("Password not created. Invalid storeType"));

                        this.inputtedText = null;
                        return new UI.UIResponse(true, mainMenu("Password store created!\nNow unlock it"));
                    })
                    .add("Cancel", () => new UI.UIResponse(true, mainMenu(null)))
            );
    }

    public UI unlockManager()
    {
        return new UI()
            .add(
                new UI.UITextBox("Unlocking manager")
            )
            .add(
                new UI.UIInput("Please enter master password for manager", (string s) => {
                    this.inputtedText = s;
                    return s;
                })
            )
            .add(
                new UI.UISelect("Enter password and choose submit. Or cancel action")
                    .add("Submit", () => {
                        var auth = new PasswordManager.PasswordManagerAuth(this.inputtedText, "session1")
                            .expiresIn(600);
                        
                        try 
                        {
                            this.manager.unlock(auth);
                        } 
                        catch (Exception e)
                        {
                            return new UI.UIResponse(true, mainMenu("Wrong password."));
                        }

                        this.inputtedText = null;
                        return new UI.UIResponse(true, managerMenu(null));
                    })
                    .add("Cancel", () => new UI.UIResponse(true, mainMenu(null)))
            );
    }
    
    public UI managerMenu(string notification)
    {
        return new UI()
            .add(
                new UI.UITextBox("Menu of your password manager")
            )
            .add(
                new UI.UITextBox(notification),
                notification != null
            )
            .add(
                new UI.UISelect("Choose what you want to do")
                    .add("Add password", () => new UI.UIResponse(true, addPasswordKey()))
                    .add("Get password", () => new UI.UIResponse(true, getPassword()))
                    .add("Show all keys password", () => {
                        string services = "";
                        foreach (var key in this.manager.keys())
                        {
                            services += key + "\n";
                        }
                        return new UI.UIResponse(true, managerMenu("All known services:\n" + services));
                    })
                    .add("Lock storage and exit", () => {
                        this.manager.lockAgain();
                        return new UI.UIResponse(true, mainMenu(null));
                    })
            );
    }

    public UI addPasswordKey()
    {
        return new UI()
            .add(
                new UI.UIInput("Type your service/website name", (string s) => {
                    this.inputtedKey = s;
                    return s;
                })
            )
            .add(
                new UI.UISelect("Choose what you want to do")
                    .add("Continue", () => new UI.UIResponse(true, addPasswordValue()))
                    .add("Cancel", () => new UI.UIResponse(true, managerMenu(null)))
            );
    }

    public UI addPasswordValue()
    {
        return new UI()
            .add(
                new UI.UIInput("Type your service/website password", (string s) => {
                    this.inputtedText = s;
                    return s;
                })
            )
            .add(
                new UI.UISelect("Choose what you want to do")
                    .add("Submit", () => {
                        this.manager.save(this.inputtedKey, this.inputtedText);

                        this.inputtedText = null;
                        return new UI.UIResponse(true, managerMenu("Password added!"));
                    })
                    .add("Cancel", () => new UI.UIResponse(true, managerMenu(null)))
            );
    }

    public UI getPassword()
    {
        return new UI()
            .add(
                new UI.UIInput("Type your service/website name", (string s) => {
                    this.inputtedKey = s;
                    return s;
                })
            )
            .add(
                new UI.UISelect("Choose what you want to do")
                    .add("Submit & Get password", () => {
                        try 
                        {
                            var password = this.manager.retreive(this.inputtedKey);
                            return new UI.UIResponse(true, managerMenu("Password to this service: " + password));
                        }
                        catch (Exception e)
                        {
                            return new UI.UIResponse(true, managerMenu("Password not found!"));
                        }
                    })
                    .add("Cancel", () => new UI.UIResponse(true, managerMenu(null)))
            );
    }
}

public class PasswordManager
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
        public abstract List<string> keys();

        public abstract void setMasterPassword(Password password);
        public abstract bool masterPasswordValid(Password possibleMasterPassword);
    }
    public class PasswordManagerStoreMemory : PasswordManagerStore
    {
        // classes

        // vars
        public string path;
        private Password masterPassword;
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

        public override List<string> keys()
        {
            var list = new List<String>();
            foreach (var item in this.passwords)
            {
                list.Add(item.Key);   
            }
            return list;
        }

        public override Password get(string key)
        {
            return passwords[key];
        }

        public override void setMasterPassword(Password password)
        {
            if (this.masterPassword != null) return;
            
            this.masterPassword = password;
        }
        public override bool masterPasswordValid(Password possibleMasterPassword)
        {
            return (
                BitConverter.ToString(this.masterPassword.value) 
                == 
                BitConverter.ToString(possibleMasterPassword.value)
            );
        }
    }

    
    // vars
    PasswordManagerStore store;
    PasswordManagerAuth auth;
    byte[] masterPassword;

    // methods
    public PasswordManager(PasswordManagerStore store)
    {
        this.store = store;
    }

    public PasswordManager unlock(PasswordManagerAuth auth)
    {
        if (!masterPasswordValid(auth.getPassword()))
            throw new AccessViolationException("password is wrong");

        this.auth = auth;
        return this;
    }

    public PasswordManager setMasterPassword(Password password)
    {
        this.store.setMasterPassword(password);
        return this;
    }

    public bool masterPasswordValid(Password password)
    {
        return this.store.masterPasswordValid(password);
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

    public List<string> keys()
    {
        if (this.auth == null) throw new ArgumentNullException("auth is null");

        return this.store.keys();
    }

    public static void main()
    {
        var store = new PasswordManager.PasswordManagerStoreMemory("passwords.json");
        var regAuth = new PasswordManager.PasswordManagerAuth("password_unlock", "session1")
            .expiresIn(10);

        var manager = new PasswordManager(store);
        manager.setMasterPassword(regAuth.getPassword());

        var auth = new PasswordManager.PasswordManagerAuth("asasas", "session1")
            .expiresIn(50);

        manager 
            .unlock(auth)
            .save("test", "testpass")
            .lockAgain();

        manager.unlock(auth);
        Console.WriteLine(manager.retreive("test"));
        manager.lockAgain();
    }
}