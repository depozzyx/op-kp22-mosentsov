using System;

public class Program
{   
    public static void Main()
    {
        try
        {
            Console.WriteLine("\nЗавдання #1 (варіант 14)");
            Task1();

            Console.WriteLine("\nЗавдання #2");
            Task2();
            
            Console.WriteLine("\nЗавдання #3");
            Task3();

            Console.WriteLine("\nЗавдання #4");
            Task4();
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Будь ласка, введіть правильне значення, за параметрами якими вас просять");
            Console.WriteLine("Опис помилки: " + e.Message);
        }
    }
    private static double readDouble(string variableName)
    {
        Console.Write("Введіть " + variableName + " : ");
        return Convert.ToDouble(Console.ReadLine());
    }


    /* Завдання #1 (варіант 14)
     *
     * Тест кейси: при n = 1 => x = 0.25 y = ~0.24
     *             при n = 4 => x = 0.25 y = ~0.24
     *                          x = 0.55 y = ~0.92
     *                          x = 0.85 y = ~1.97
     *                          x = 1.15 y = ~3.34
     */
    public static void Task1()
    {
        uint n = (uint) Math.Floor(readDouble("натуральне число - кількість проміжних значень"));
        double xk = 5.2;
        double x0 = 0.25;
        double dx = 0.3;
        double b = 0.8;
        
        double x = x0;
        for (int i = 0; i < n; i++)
        {   
            double y = Math.Pow(x, 2.5 - b) * Math.Log(Math.Pow(x, 2) + 12.7);
            Console.WriteLine("При x = " + x + " y = " + y);

            x += dx;

            if (x >= xk)
            {
                Console.WriteLine("Досягнуте значення xk, виходимо з циклу");
                break;
            }
        }
    }


    /* Завдання #2
     *
     * Тест кейси: при n = 0 => Число не є простим
     *             при n = 0.2 => *Помилка*
     *             при n = -13 => Число не є простим
     *             при n = 13 => Число є простим
     *             при n = 1 => Число є простим
     *             при n = 14 => Число не є простим
     *             при n = 1200 => Число не є простим
     */
    public static void Task2()
    {
        long n = (long) readDouble("ціле число");
        bool isPrime = mathIsPrime(n);
        Console.WriteLine("Число " + n + " " + (isPrime ? "є простим" : "не є простим"));
    }
    private static bool mathIsPrime(long number)
    {   
        if (number <= 0) return false;
        for (int i = 2; i < (Math.Abs(number) / 2); i += 1)
        {
            if ((number % i) == 0) return false;
        }
        return true;
    }


    /* Завдання #3
     *
     * Тест кейси: при f = -1 => *Помилка*
     *             при f = 0 => 1
     *             при f = 1 => 1
     *             при f = 5 => 120
     *             при x = 0, n = 0 => *Помилка*
     *             при x = 109, n = 0 => 1
     *             при x = 1, n = 5 => 1
     *             при x = 2, n = 5 => 32
     *             при x = -2, n = 6 => 64
     *             при x = 3, n = -2 => 1/9
     */
    public static void Task3()
    {
        double f = readDouble("аргумент факторіалу");
        Console.WriteLine(f + "! = " + mathFactorial(f));

        double x = readDouble("базу x степеня x^n");
        int n = (int) readDouble("цілий степінь n степеня x^n");
        Console.WriteLine(x + "^" + (n >= 0 ? ("" + n) : ("(" + n + ")")) + " = " + mathPow(x, n));
    }
    private static double mathFactorial(double number) 
    {
        // ми рекурсію не вчили, але через неї
        // красиво раїується факторіал
        if (number < 0) throw new ArgumentException(number + "! is not defined");
        if (number == 0) return 1;
        if (number == 1) return number; 
        return (double) mathFactorial(number - 1) * number;
    }
    private static double mathPow(double powBase, int n) 
    {
        if (powBase == 0 && n == 0) throw new ArgumentException("0^0 is not defined");

        bool isNegative = false;
        if (n < 0) 
        {
            n = -n;
            isNegative = true;
        }

        double result = 1;
        for (int _ = 0; _ < n; _ += 1) result *= powBase;

        return isNegative ? 1/result : result;
    }


    /* Завдання #3
     *
     * Тест кейси: Саме завдання
     */
    public static void Task4()
    {
        double[] testCases = {1, 2, 3.14, 1.58, -3.14, 0, 3.14 + 1.58, 5, 12, -10, 8, 4, 6.38};
        uint prescision = (uint) readDouble("точність ф-ції sin (число більше 2, рекомендовано: 10)");

        Console.WriteLine("\nТестові сценарії(" + testCases.Length + ") :");
        Console.WriteLine("sin(x) = {моя ф-ція} || {бібліотечна ф-ція}");
        // мабуть не вчили foreach, але тут він найкращий
        foreach (double x in testCases) 
        {
            Console.WriteLine("sin(" + x + ") = " + mathSin(x, prescision) + " || " + Math.Sin(x));
        }

    }
    private static double mathSin(double x, uint prescision = 10)
    {
        // якщо більше PI*2 то цей алгоритм погано працює
        // тому нормалізую аргументи до [-PI*2, PI*2]
        x = x % (Math.PI * 2); 

        double result = 0;
        for (int i = 0; i < prescision; i++)
        {
            int k = 2 * i + 1;
            int sign = (i % 2 == 0) ? 1 : -1;
            result += sign * (mathPow(x, k) / mathFactorial(k));
        }

        return result;
    }
}
