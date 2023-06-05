using System;
using System.Collections.Generic;

class Program 
{
    static void Main(String[] args) 
    {
        var store = new PasswordManagerStoreJSON("passwords.json");
        var manager = new PasswordManager(store);
        manager.unlock();
        var key = manager.retreive("test");
    }
}

class UI 
{

}

class PasswordManager
{
    // classes
    class Password 
    {
        // classes
        abstract class PasswordAlghorithm {
            public abstract string hash(string value)
        }

        class PasswordAlghorithmRSA : PasswordAlghorithm 
        {
            public string hash(string value)
            {
                return "";
            }
        }

        // vars
        public PasswordAlghorithm alghorithm;
        public string value;

        // methods
    }

    class PasswordManagerAuth 
    {
        // classes

        // vars
        public Password password;
        public expiresOn uint;

        // methods
    }

    abstract class PasswordManagerStore 
    {
        // classes

        // vars

        // methods
        public abstract void save(PasswordManager state);
        public abstract PasswordManager get();
    }
    class PasswordManagerStoreJSON : PasswordManagerStore
    {
        // classes

        // vars
        public string path;

        // methods
        public PasswordManagerStoreJSON(string path)
        {
            this.path = path;
        }

        public void save(PasswordManager state)
        {

        }

        public PasswordManager get()
        {
            return new PasswordManager(null);
        }
    }
    
    // vars
    PasswordManagerStore store;
    PasswordManagerAuth auth;
    bool isLocked;

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

    public PasswordManager store(string key, string password)
    {
        return this;
    }

    public Password retreive(string key)
    {
        return new Password();
    }
}

class TestUI 
{

}

class TestPasswordManager 
{

}