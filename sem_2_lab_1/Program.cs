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

    /* Завдання #1 
     *
     * Критерії роботи: 
     *  1. У файл записалося 2 строки
     *  2. Правильна кодировка у файлі
     *  3. Користовачу вивелось 2 строки
     *  4. Запис зробився у файл task1.txt
     *
     */
    static void Task1()
    {
        string text = "Перша строка\nДруга строка";
        Console.WriteLine(text);
        File.WriteAllText("task1.txt", text);
    }

    /* Завдання #2 
     *
     * Критерії роботи: 
     *  1. Всього 15 чисел 
     *  2. Усі числа рандомні float-и 
     *  3. Запис робиться у файл task2.txt
     *  4. Запис максимального робиться у файл max.txt 
     *
     */
    static void Task2()
    {
        System.Random random = new System.Random();
        string numbers = "";
        double max = -1;
        for (int i = 0; i < 15; i++)
        {
            double number = Math.Round(random.NextDouble() * 100, 2);

            if (i == 0) max = number;
            else if (number > max) max = number;

            numbers += number.ToString() + " "; 
        }
        File.WriteAllText("task2.txt", numbers);
        File.WriteAllText("max.txt", max.ToString());
    }

    /* Завдання #3 
     *
     * Критерії роботи: 
     *  1. Є файл з 40 словами, які не більше 80 символів 
     *  2. На виході маємо відсортований на алфавітом слів файл 
     *  3. Запис робиться у файл task3.txt
     *
     */
    static void Task3()
    {
        string readText = File.ReadAllText("input_task3.txt");
        Console.WriteLine(readText); 
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
