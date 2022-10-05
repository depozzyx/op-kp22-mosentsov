
using System;

public class Program
{
    public static void Main() 
    {
        Task5();
        Task6();
    }

    public static void Task5()
    {
        Console.WriteLine("\n\n#5 Розробити алгоритм і програму визначення об’ємів циліндра і конуса з радіусом основи R = 5 см і висотою H = 8 см.:");

        const double r = 5;
        const double h = 8;

        double vc = h * Math.PI * r * r;
        Console.WriteLine("V(циліндра) = " + Math.Round(vc));
        Console.WriteLine("V(конуса) = " + Math.Round(vc / 3));
    }

    public static void Task6()
    {
        Console.WriteLine("\n\n#6 Розробити алгоритм і програму визначення спільного опору електричного ланцюга, якщо є три резистора R1, R2, R3.:");

        Console.Write("Введіть опір R1: ");
        double r1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть опір R2: ");
        double r2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть опір R3: ");
        double r3 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Спільний опір: " + Math.Round((r1 * r2 * r3) / (r2 * r3 + r1 * r3 + r1 * r2), 3));
    }
}