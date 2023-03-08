using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
    static private List<string> Split(string str, char delitimer)
    {
        List<string> result = new List<string>();

        int fragmentStart = 0;
        int fragmentNumber = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == delitimer) {
                result.Add(str.Substring(fragmentStart, i - fragmentStart));
                fragmentStart = i + 1;
                fragmentNumber += 1;
            }
        }
        if (fragmentStart != str.Length) {
            result.Add(str.Substring(fragmentStart, str.Length - fragmentStart));
        }

        return result;
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
        File.WriteAllText("task1.txt", string.Join("--", Split(text, ' ')));
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
        string allWords = File.ReadAllText("input_task3.txt");
        List<string> splitted = Split(allWords, '\n');

        bool isSorted = false;
        while (!isSorted) {
            isSorted = true;

            for (int i = 0; i < splitted.Count - 1; i++)
            {
                if (wordLess(splitted[i + 1], splitted[i])) {
                    string temp = splitted[i];
                    splitted[i] = splitted[i + 1];
                    splitted[i + 1] = temp;

                    isSorted = false;
                }
            }
        }

        foreach (var word in splitted)
        {
            Console.Write(word + " ");   
        }
        Console.WriteLine("\n---"); 

    }
    /*
     * Допоміжна ф-ція для завдання 3. Перевіряє чи менше за алфавітом слово a ніж слово b
     */
    static private bool wordLess(string wordA, string wordB)
    {   
        wordA = wordA.ToLower();
        wordB = wordB.ToLower();
        int lengthA = wordA.Length;
        int lengthB = wordB.Length;
        for (int i = 0; i < Math.Min(lengthA, lengthB); i++)
        {
            if (wordA[i] < wordB[i]) return true;
            else if (wordA[i] > wordB[i]) return false;
        }

        return lengthA < lengthB;
    }
    /* Завдання #4
     * Критерії роботи:
     * 1. Файл читається правильно та студенти правильно ідентификуються
     * 2. Показуються студенти, в яких score < 60
     * 3. Якщо поганих студентів немає, Показуються відповідне повідомлення
     */
    const string TASK4_INPUT_FILENAME = "input_task4.csv";
    static void Task4()
    {
        string csv = File.ReadAllText(TASK4_INPUT_FILENAME);
        List<string> students = Split(csv, '\n');
        bool hasBadStudents = false;

        foreach (var student in students)
        {
            List<string> info = Split(student, ',');
            string firstName = info[0],
                   lastName = info[1];
            int score = Int16.Parse(info[2]);

            if (score < 60) {
                Console.WriteLine(firstName + " " + lastName + ": " + score + " - Поганий результат");
                hasBadStudents = true;
            }
        }

        if (!hasBadStudents) {
            Console.WriteLine("Студентів з рахунком <60 немає!");
        }

    }

    /* Завдання #5
     * Критерії роботи:
     * 1. Текст файла читається
     * 2. Рахується кількість слів між пробілами
     * 3. Рахується кількість слів між іншими символами
     */
    const string TASK5_INPUT_FILENAME = "input_task5.txt";
    static void Task5()
    {
        string text = File.ReadAllText(TASK5_INPUT_FILENAME);

        // Вбудований Split не можна, але про регулярні вирази нічого не казали
        Regex rx = new Regex(@"(\w|-)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        MatchCollection matches = rx.Matches(text);

        Console.WriteLine("Кількість слів: " + matches.Count);
    }

    /* Завдання #6
     * Критерії роботи:
     */
    const string TASK6_FILENAME = "task6.bin";
    const string TASK6_GOOD_STUDENTS_FILENAME = "task6_goodStudents.bin";
    static void Task6()
    {
        {
            BinaryWriter bw;
            try {
                bw = new BinaryWriter(new FileStream(TASK6_FILENAME, FileMode.Create));
            } catch (IOException e) {
                Console.WriteLine("Can't create file task6.bin. Maybe file exists?");
                return;
            }
            string csv = File.ReadAllText(TASK4_INPUT_FILENAME);
            bw.Write(csv);
            bw.Close();
        }

        {
            BinaryReader br;
            try {
                br = new BinaryReader(new FileStream(TASK6_FILENAME, FileMode.Open));
            } catch (IOException e) {
                Console.WriteLine("Can't open file task6.bin. Error: " + e);
                return;
            }
            string csvNew = br.ReadString();

            List<string> students = Split(csvNew, '\n');
            List<string> goodStudents = new List<string>();
            foreach (var student in students)
            {
                List<string> info = Split(student, ',');
                int score = Int16.Parse(info[2]);

                if (score > 95) {
                    goodStudents.Add(student);
                }
            }

            BinaryWriter bw;
            try {
                bw = new BinaryWriter(new FileStream(TASK6_GOOD_STUDENTS_FILENAME, FileMode.Create));
            } catch (IOException e) {
                Console.WriteLine("Can't create file task6_goodStudents.bin Maybe file exists?");
                return;
            }
            bw.Write(goodStudents.Count);
            foreach (var goodStudent in goodStudents)
                bw.Write("\n" + goodStudent);
            bw.Close();
        }

    }
}
