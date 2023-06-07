using System;

class Program
{
    static public void Main()
    {
        Deque<string>.main(null);
        RandomizedQueue<string>.main(null);
    }
}

// Dequeue
public class Deque<Item> : IIterable<Item> {
    private LinkedList<Item> list;

    // construct an empty deque
    public Deque()
    {
        this.list = new LinkedList<Item>();
    }

    public override string ToString()
    {
        return "Deque(" + this.list + ")";
    }

    public void Print()
    {
        Console.WriteLine(this.ToString());
    }

    // is the deque empty?
    public bool isEmpty()
    {
        return this.list.head == null;
    }

    // return the number of items on the deque
    public int size()
    {
        return (int)this.list.size;
    }

    // add the item to the front
    public void addFirst(Item item)
    {
        this.list.ReplaceHead(item);
    }

    // add the item to the back
    public void addLast(Item item)
    {
        this.list.Add(item);
    }

    // remove and return the item from the front
    public Item removeFirst()
    {
        return this.list.Remove(0);
    }

    // remove and return the item from the back
    public Item removeLast()
    {
        return this.list.Remove(this.list.size - 1);
    }

    // return an iterator over items in order from front to back
    public IIterator<Item> iterator()
    {
        return new IteratorImpl(this.list);
    }

    private class IteratorImpl : IIterator<Item> 
    {
        ListNode<Item> head;

        public IteratorImpl(LinkedList<Item> list)
        {
            this.head = list.head;
        }
        public bool HasNext {get { return head != null && head.next != null; } }
        public Item MoveNext()
        {
            if (head == null) return default(Item);
            this.head = head.next;
            return this.head.data;
        }
    }

    // unit testing (required)
    public static void main(String[] args)
    {
        var deque = new Deque<string>();
    }
}


// Randomized queue
public class RandomizedQueue<Item> : IIterable<Item> {
    private LinkedList<Item> list;

    // construct an empty randomized queue
    public RandomizedQueue()
    {
        this.list = new LinkedList<Item>();
    }

    public override string ToString()
    {
        return "RandomizedQueue(" + this.list + ")";
    }

    public void Print()
    {
        Console.WriteLine(this.ToString());
    }

    // is the randomized queue empty?
    public bool isEmpty()
    {
        return this.list.head == null;
    }

    // return the number of items on the randomized queue
    public int size()
    {
        return (int)this.list.size;
    }

    // add the item
    public void enqueue(Item item)
    {
        list.Add(item);
    }

    // remove and return a random item
    public Item dequeue()
    {
        if (this.list.size == 0) return default(Item);
        
        return this.list.Remove(randIndex());
    }

    // return a random item (but do not remove it)
    public Item sample()
    {
        if (this.list.size == 0) return default(Item);

        return this.list.Find(randIndex());
    }

    private uint randIndex()
    {
        var rand = new Random();
        return (uint)rand.Next(0, (int)this.list.size);
    }

    // return an independent iterator over items in random order
    public IIterator<Item> iterator()
    {
        return new Iterator(this.list);
    }

    private class Iterator : IIterator<Item> 
    {
        LinkedList<Item> list;

        public Iterator(LinkedList<Item> list)
        {
            this.list = list;
        }
        public bool HasNext {get { return list.head != null && list.head.next != null; } }
        public Item MoveNext()
        {
            if (list.head == null) return default(Item);

            var rand = new Random();
            var index = (uint)rand.Next(0, (int)this.list.size);

            return list.Remove(index);
        }
    }

    // unit testing (required)
    public static void main(String[] args)
    {
        var queue = new RandomizedQueue<string>();
    }
}

// Linked list
public class ListNode<T> 
{
    public T data;
    public ListNode<T> next;

    public ListNode(T data) 
    {
        this.data = data;
    }

    public override string ToString()
    {
        return "Node('" + data + "')";
    }
}


public class LinkedList<T> 
{
    public uint size;
    public ListNode<T> head;
    public ListNode<T> tail;

    public LinkedList() 
    {

    }

    public override string ToString()
    {
        var node = this.head;
        string res = "LinkedList(";
        while (node != null) 
        {
            res += node.ToString() + " -> ";
            node = node.next;
        }
        res += "null; head: " + this.head + "; tail: " + this.tail + "; size: " + this.size + ")";
        return res;
    }

    public void Add(T value) 
    {
        this.size += 1;

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

    public void ReplaceHead(T value)
    {
        this.size += 1;

        var node = new ListNode<T>(value);
        node.next = this.head;
        this.head = node;

        if (this.tail == null)
        {
            this.tail = node;
        }
    }

    public T Remove(uint index) 
    {
        uint i = 0;
        ListNode<T> prev = null;
        var node = this.head;
        
        if (node == null)
        {
            return default(T);
        }

        if (index == 0 && node.next == null) 
        {
            this.size -= 1;

            this.tail = null;
            this.head = null;
            return node.data;
        }

        while (node != null) 
        {
            if (i == index) 
            {
                if (prev == null) {
                    this.head = node.next;
                } else {
                    prev.next = null;
                    if (node.next != null) 
                        prev.next = node.next;
                }
                if (i == this.size - 1) 
                {
                    this.tail = prev;
                }
                
                this.size -= 1;
                return node.data;
            }
            prev = node;
            node = node.next;
            i += 1;
        }
        return default(T);
    }

    public T Find(uint index)
    {
        uint i = 0;

        var node = this.head;

        if (node == null) 
        {
            return default(T);
        }

        if (node.next == null && index == 0)
        {
            return node.data;
        }

        while (node != null) 
        {
            if (i == index) 
                return node.data;

            node = node.next;
            i += 1;
        }
        return default(T);
    }

    public IIterator<T> Iter()
    {
        return new Iterator(this.head);
    }

    private class Iterator : IIterator<T> 
    {
        ListNode<T> head;

        public Iterator(ListNode<T> head)
        {
            this.head = head;
        }
        public bool HasNext {get { return head != null && head.next != null; } }
        public T MoveNext()
        {
            if (head == null) return default(T);
            return head.next.data;
        }
    }
}

// Iterators
public interface IIterable<T>
{
    IIterator<T> iterator();
}

public interface IIterator<T>
{
    bool HasNext { get; }
    T MoveNext();
}