using System;

public class Program
{
    public static void Main()
    {
        Task1();
        Task2();
        Task3();
    }

    private static double readDouble(string name)
    {
        Console.Write("Введіть " + name + ": ");
        return Convert.ToDouble(Console.ReadLine());
    }

    public static void Task1()
    {
        Console.WriteLine("\n1. знайти мінімум з 4 цілочисельних чисел");     
        double a1 = readDouble("перше число");
        double a2 = readDouble("друге число");
        double a3 = readDouble("третє число");
        double a4 = readDouble("четверте число");

        double min = a1;
        if (a2 < min) min = a2;
        if (a3 < min) min = a3;
        if (a4 < min) min = a4;

        Console.WriteLine("Мінинмалье число: " + min);
    }

    public static void Task2()
    {
        Console.WriteLine("\n2. Знайти площу трикутника за формулою Герона");     
        double a1 = readDouble("перша сторона");
        double a2 = readDouble("друга сторона");
        double a3 = readDouble("третя сторона");

        double p = (a1 + a2 + a3) / 2;
        double s = Math.Sqrt(p * (p - a1) * (p - a2) * (p - a3));
        Console.WriteLine("Площа: " + s);
    }

    public static void Task3()
    {
        Console.WriteLine("\n3. а координатами двох точок( x1; y1) (x2; y2) знайти модуль вектора(його довжину)");     
        double x1 = readDouble("x1");
        double y1 = readDouble("y1");
        double x2 = readDouble("x2");
        double y2 = readDouble("y2");

        double xv = x2 - x1;
        double yv = y2 - y1;
        double v = Math.Sqrt(xv * xv + yv * yv);
        Console.WriteLine("Модуль вектора: " + v);
    }
}