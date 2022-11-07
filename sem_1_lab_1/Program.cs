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

    }


    public static void Task3()
    {
        double f = readDouble("аргумент факторіалу");
        Console.WriteLine(f + "! = " + mathFactorial(f));

        double x = readDouble("базу x степеня x^n");
        int n = (int) readDouble("степінь n степеня x^n");
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
        for (int _ = 0; _ < n; _++) result *= powBase;

        return isNegative ? 1/result : result;
    }


    public static void Task4()
    {

    }
}
