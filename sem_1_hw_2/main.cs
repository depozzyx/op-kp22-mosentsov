using System;

public class Program
{
    public static void Main()
    {
        Console.Write("a = ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Choose a sign (+, -, *, /) = ");
        string sign = Console.ReadLine().Trim().Substring(1);

        Console.Write("b = ");
        double b = Convert.ToDouble(Console.ReadLine());
        

        double result = -1;
        if (sign == "+") {
            result = a + b;
        } else if (sign == "-") {
            result = a - b;
        } else if (sign == "*") {
            result = a * b;
        } else if (sign == "/") {
            result = a / b;
        }
        Console.WriteLine(a + " " + sign + " " + b + " = " + result);
    }
}