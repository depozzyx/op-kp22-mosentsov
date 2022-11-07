using System;

public class Program
{   
    public static void Main()
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
    private static double readDouble(string variableName)
    {
        Console.Write("Введіть " + variableName + " : ");
        return Convert.ToDouble(Console.ReadLine());
    }


    public static void Task1()
    {

    }


    public static void Task2()
    {
        long n = (long) readDouble("ціле число");
        bool isPrime = mathIsPrime(n);
        Console.WriteLine("Число " + n + " " + (isPrime ? "є простим" : "не є простим"));
    }
    private static bool mathIsPrime(long number)
    {   
        for (int i = 2; i < (Math.Abs(number) / 2); i += 1)
        {
            if ((number % i) == 0) return false;
        }
        return true;
    }


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


    public static void Task4()
    {
        double[] testCases = {1, 2, 3.14, 1.58, 0, 3.14 + 1.58, 5, 12, 10, 8, 6, 4, 6.38};
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
