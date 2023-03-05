using System;
using System.IO;

class TextFiles {
    static void Main(String[] args)
    {
        Console.WriteLine("\nЗавдання #1");
        Task1();

        Console.WriteLine("\nЗавдання #2");
        Task2();

        Console.WriteLine("\nЗавдання #3");
        Task3();

        Console.WriteLine("\nЗавдання #4");
        Task4();

        Console.WriteLine("\nЗавдання #5");
        Task5();

        Console.WriteLine("\nЗавдання #6");
        Task6();
    }

    static void Task1()
    {
        string text = "Перша строка\nДруга строка";
        Console.WriteLine(text);
        File.WriteAllText("task1.txt", text);
    }

    static void Task2()
    {

    }

    static void Task3()
    {

    }

    static void Task4()
    {

    }

    static void Task5()
    {

    }

    static void Task6()
    {

    }
}