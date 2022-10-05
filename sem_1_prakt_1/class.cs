using System;

public class Program
{
    public static void Main() 
    {
        // Task0();
        Task1();
        Task2();
        Task3();
        Task4();
    }
    public static void Task0() 
    {
        Console.Write("a=");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("b=");
        double b = Convert.ToDouble(Console.ReadLine());

        double c = Math.Round(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)), 2);
        Console.WriteLine("c=" + c);
    }

    public static void Task1()
    {
        Console.WriteLine("\n\n#1 Розробити алгоритм і програму, в якої підраховується кількість годин, хвилин і секунд в заданій кількості діб");
        Console.Write("Введіть кількість діб: ");
        double days = Convert.ToDouble(Console.ReadLine());

        double hours = days * 24;
        Console.WriteLine("Години: " + hours);

        double minutes = hours * 60;
        Console.WriteLine("Хвилини: " + minutes);

        double seconds = minutes * 60;
        Console.WriteLine("Секунди: " + seconds);
    }
    public static void Task2()
    {
        Console.WriteLine("\n\n#2 Розробити алгоритм і програму, в якій обчислюється площа трикутника по трьом сторонам. Обчислення проводиться по формулі Герона.");

        Console.Write("Введіть сторону трикутника A: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть сторону трикутника B: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть сторону трикутника C: ");
        double c = Convert.ToDouble(Console.ReadLine());

        double p = (a + b + c) / 2;
        double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        Console.WriteLine("Площа цього трикутника: " + Math.Round(s, 3));
    }
    public static void Task3()
    {
        Console.WriteLine("\n\n#3 Розробити алгоритм і програму, в якої обчислюються площа і об’єм сфери");
        Console.Write("Введіть радіус сфери: ");
        double r = Convert.ToDouble(Console.ReadLine());

        double s = 4 * Math.PI * r * r;
        Console.WriteLine("S = " + Math.Round(s, 3));

        double v = (s * r) / 3;
        Console.WriteLine("V = " + Math.Round(v, 3));

    }
    public static void Task4()
    {
        Console.WriteLine("\n\n#4 Розробити алгоритм і програму рішення системи лінійних рівнянь:");

        Console.Write("Введіть коеф. a1 ");
        double a1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коеф. a2 ");
        double a2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коеф. b1 ");
        double b1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коеф. b2 ");
        double b2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коеф. c1 ");
        double c1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коеф. c2 ");
        double c2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("X = " + Math.Round((c1 * b1 - c2 * b2 ) / (a1 * b1 - a2 * b2), 3));
    }
}