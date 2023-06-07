Object-oriented programming. Sem 2. Assignment 3.

Write a generic data type for a deque and a randomized queue. The goal of this assignment is to implement elementary data structures using arrays and linked lists, and to introduce you to generics and iterators.
Dequeue. A double-ended queue or deque (pronounced “deck”) is a generalization of a stack and a queue that supports adding and removing items from either the front or the back of the data structure. Create a generic data type Deque that implements the following API:
public class Deque<Item> : IIterable<Item> {

    // construct an empty deque
    public Deque()

    // is the deque empty?
    public boolean isEmpty()

    // return the number of items on the deque
    public int size()

    // add the item to the front
    public void addFirst(Item item)

    // add the item to the back
    public void addLast(Item item)

    // remove and return the item from the front
    public Item removeFirst()

    // remove and return the item from the back
    public Item removeLast()

    // return an iterator over items in order from front to back
    public IIterator<Item> iterator()

    private class IteratorImpl : IIterator<T>
    {
       bool HasNext { … }
       T MoveNext() { … }
    }

    // unit testing (required)
    public static void main(String[] args)

}

public interface IIterable<T>
{
public IIterator<Item> iterator()
}

public interface IIterator<T>
{
bool HasNext { get; }
T MoveNext();
}

Performance requirements. Your deque implementation must support each deque operation (including construction) in constant worst-case time. Additionally, your iterator implementation must support each operation (including construction) in constant worst-case time.
A randomized queue is similar to a stack or queue, except that the item removed is chosen uniformly at random among items in the data structure. Create a generic data type RandomizedQueue that implements the following API:

public class RandomizedQueue<Item> : IIterable<Item> {

    // construct an empty randomized queue
    public RandomizedQueue()

    // is the randomized queue empty?
    public boolean isEmpty()

    // return the number of items on the randomized queue
    public int size()

    // add the item
    public void enqueue(Item item)

    // remove and return a random item
    public Item dequeue()

    // return a random item (but do not remove it)
    public Item sample()

    // return an independent iterator over items in random order
    public Iterator<Item> iterator()

    // unit testing (required)
    public static void main(String[] args)

}

Iterator. Each iterator must return the items in uniformly random order. The order of two or more iterators to the same randomized queue must be mutually independent; each iterator must maintain its own random order.

Unit testing. Your main() method must call directly every public constructor and method to verify that they work as prescribed (e.g., by printing results to standard output).
Performance requirements. Your randomized queue implementation must support each randomized queue operation (besides creating an iterator) in constant amortized time. That is, any intermixed sequence of m randomized queue operations (starting from an empty queue) must take at most cm steps in the worst case, for some constant c. Additionally, your iterator implementation must support operations next() and hasNext() in constant worst-case time; and construction in linear time; you may (and will need to) use a linear amount of extra memory per iterator.

DEADLINE: May 25, max - 20 points
