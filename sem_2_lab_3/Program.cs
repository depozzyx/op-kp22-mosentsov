using System;

class Program
{
    static public void Main()
    {
        new SpellingTool();
    }

}

public class SpellingTool 
{
    public HashTable<string, bool> words;

    public SpellingTool() 
    {
        this.words = new HashTable<string, bool>(100);
        initWords();

        Console.WriteLine("============");
        Console.WriteLine("Welcome to spelling tool!");
        Console.WriteLine("");
        Console.WriteLine("Write any amount of words (new lines supported) and you");
        Console.WriteLine("will get notification when someting's wrong");
        Console.WriteLine("");
        Console.WriteLine("Write 'exit' to exit the program");
        Console.WriteLine("============");

        while (true) 
        {
            string line = Console.ReadLine();
            if (line == "exit") 
            {
                Console.WriteLine("============ Exited ============");
                break;
            }

            var enteredWords = line.Split(" "); 
            bool isError = false;
            foreach (var word in enteredWords)
            {
                if (!words.Contains(word.ToLower())) 
                {
                    if (!isError) 
                    {
                        Console.WriteLine("============\nError(s) found:");
                    }
                    Console.WriteLine(" [*] Word '" + word + "' not found in dictionary");
                    isError = true;
                }
            }
            if (isError) Console.WriteLine("============");
        }
    }

    private void initWords()
    {
        newWords(new string[]{
            "the", "at", "there", "some", "my",
            "of", "be", "use", "her", "than",
            "and", "this", "an", "would", "first",
            "a", "have", "each", "make", "water",
            "to", "from", "which", "like", "been",
            "in", "or", "she", "him", "call",
            "is", "one", "do", "into", "who",
            "you", "had", "how", "time", "oil",
            "that", "by", "their", "has", "its",
            "it", "word", "if", "look", "now",
            "he", "but", "will", "two", "find",
            "was", "not", "up", "more", "long",
            "for", "what", "other", "write", "down",
            "on", "all", "about", "go", "day",
            "are", "were", "out", "see", "did",
            "as", "we", "many", "number", "get",
            "with", "when", "then", "no", "come",
            "his", "your", "them", "way", "made",
            "they", "can", "these", "could", "may",
            "i", "said", "so", "people", "part",
        });
    }

    private void newWords(string[] newWordsArr)
    {
        foreach (var word in newWordsArr)
        {
            words.Add(word, true);  
        }
    }
}

public class HashTable<KItem, VItem> where KItem: IConvertible
{
    private uint bucketsSize;
    private HashBucket<VItem>[] buckets;

    public HashTable() 
    {
        this.bucketsSize = 256;
        this.buckets = new HashBucket<VItem>[this.bucketsSize];
    }
    public HashTable(uint intialCapacity) 
    {
        this.bucketsSize = intialCapacity;
        this.buckets = new HashBucket<VItem>[this.bucketsSize];
    }

    public void Add(KItem key, VItem data) 
    {
        uint index = this.hash(key);

        if (buckets[index] == null) 
        {
            buckets[index] = new HashBucket<VItem>(index);
        }

        buckets[index].Add(Convert.ToString(key), data);
    }

    public void Remove(KItem key) 
    {
        uint index = this.hash(key);

        if (buckets[index] == null) return;

        buckets[index].Remove(Convert.ToString(key));
    }

    public VItem Get(KItem key) 
    {
        uint index = this.hash(key);

        if (buckets[index] == null) return default(VItem);

        return buckets[index].Get(Convert.ToString(key));
    }

    public bool Contains(KItem key) 
    {
        uint index = this.hash(key);

        if (Get(key) == null) return false;

        return true;
    }

    public void clear()
    {
        this.buckets = new HashBucket<VItem>[this.bucketsSize];
    }

    public int size()
    {
        return this.buckets.Length;
    }

    private uint hash(KItem key) 
    {
        uint sum = 0;
        string skey = Convert.ToString(key);
        foreach (var chr in skey)
        {
            sum += (uint) chr;
        }
        return sum % this.bucketsSize;
    }
}


public class HashBucket<T> 
{
    public uint index;
    private LinkedList<KeyValue> nodes;

    public struct KeyValue 
    {
        public string key;
        public T data;

        public KeyValue(string key, T data) 
        {
            this.key = key;
            this.data = data;
        }
    }

    public HashBucket(uint index) 
    {
        this.index = index;
        this.nodes = new LinkedList<KeyValue>();
    }

    public void Add(string key, T data) 
    {
        this.Remove(key);

        var kv = new KeyValue(key, data);
        this.nodes.Add(kv);
    }

    public void Remove(string key) 
    {
        var node = nodes.head;
        uint i = 0;

        while (node != null) 
        {
            if (node.data.key == key) 
            {
                this.nodes.Remove(i);
                return;
            }
            i += 1;
            node = node.next;
        }
    }

    public T Get(string key) 
    {
        var node = nodes.head;

        while (node != null) 
        {
            if (node.data.key == key) 
            {
                return node.data.data;
            }
            node = node.next;
        }
        return default(T);
    }
}

public class ListNode<T> 
{
    public T data;
    public ListNode<T> next;

    public ListNode(T data) 
    {
        this.data = data;
    }
}


public class LinkedList<T> 
{
    public ListNode<T> head;
    public ListNode<T> tail;

    public LinkedList() 
    {

    }

    public void Add(T value) 
    {
        var node = new ListNode<T>(value);
        if (this.head == null) 
        {
            this.head = node;
        }
        if (this.tail != null) 
        {
            this.tail.next = node;
        }
        this.tail = node;
    }

    public void Remove(uint index) 
    {
        uint i = 0;
        ListNode<T> prev = null;
        var node = this.head;

        if (node.next == null) 
        {
            this.tail = null;
            this.head = null;
            return;
        }

        while (node != null) 
        {
            if (i == index) 
            {
                prev.next = null;
                if (node.next != null) 
                {
                    prev.next = node.next;
                }
                return;
            }
            prev = node;
            node = node.next;
            i += 1;
        }
    }
}