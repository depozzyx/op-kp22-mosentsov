Object-oriented programming. Sem 2. Assignment 4.

In class, the Set Abstract Data Type was covered and how to use Hash Tables to implement the Set ADT. One natural use of a set is to hold the words in a spelling dictionary. This makes looking up the words very efficient. In this assignment, you will implement and use such a dictionary. The interface of the Hashtable class is below:

public class HashTable<KItem, VItem>
{

        public HashTable()
        { ... }

        public HashTable(int intialCapacity)
        { ... }

        public void Add(KItem key, VItem value)
        {
           ...
        }

        public void Remove(KItem key)
        {
           ...
        }

        public VItem Get(KItem key)
        {
            ...
        }

        public boolean Contains(VItem key)
        {
            ...
        }

        public void clear()
        {
           ...
        }

        public int size()
        {
           ...
        }

}

Choose one of four methods for resolving collisions that were discussed in the lecture. Be ready to explain all 4 methods.

The second part of the assignment is to write a main program that uses your hash table class. The main program will behave like the standard Linux utility program ispell, when that program is used interactively to check individual words. To try it, enter the command ispell on the command line. You will be prompted to enter words.
But we will implement a simplified version.
Your program will return ok, if the word is in the dictionary and WrongSpelling if the word is not present in the dictionary. The dictionary should contain at least 50 words.

Deadline: June 10
