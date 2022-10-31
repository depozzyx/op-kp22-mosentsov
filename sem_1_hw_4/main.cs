using System;

public class Program
{
    public static void Main()
    {
        Task1();
        Task2();
    }


    // Task 1 stuff
    private static void writeString(int numberOfTimes, string str) 
    {
        for (int i = 0; i < numberOfTimes; i += 1)
        {
            Console.Write(str);   
        }
    }
    public static void Task1()
    {
        Console.Write("Введіть ціле число (>=1): ");
        int times = Convert.ToInt16(Console.ReadLine());

        writeString(1, "#");
        writeString((times + 1) * 4 , "=");
        writeString(1, "#\n");

        for (int i = 0; i < times; i += 1)
        {
            // TODO: make separate function to inner loop
            writeString(times - i, "  ");
            writeString(1, "|<>");
            writeString(i, "....");
            writeString(1, "<>|");
            writeString(times - i, "  ");
            writeString(1, "\n");
        }
        for (int i = times - 1; i >= 0; i -= 1)
        {
            // TODO: make separate function to inner loop
            writeString(times - i, "  ");
            writeString(1, "|<>");
            writeString(i, "....");
            writeString(1, "<>|");
            writeString(times - i, "  ");
            writeString(1, "\n");
        }

        writeString(1, "#");
        writeString((times + 1) * 4 , "=");
        writeString(1, "#\n");
    }

    // Task 2 stuff
    const double prescision = 0.001;
    private static double pow(double number, int times)
    {
        double res = 1;
        for (int i = 0; i < times; i++)
        {
            res *= number;   
        }
        return res;
    }
    public static void Task2()
    {
        Console.Write("Введіть степінь кореня (>=2): ");
        int n = Convert.ToInt16(Console.ReadLine());

        Console.Write("Введіть число з якого взяти корень (>=1): ");
        double number = Convert.ToInt32(Console.ReadLine());

        double l = 1;
        double r = number / 2 + 1;

        double ourNumber = 1;
        double res = 1;
        while (Math.Abs(ourNumber - number) > prescision)
        {
            res = (l + r) / 2;
            ourNumber = pow(res, n);
            // Console.WriteLine(ourNumber + " / " + res + "[" + l + " " + r + "]");

            if (ourNumber > number) r = res;
            else l = res;
        }
        // Console.WriteLine("{0:N3}^" + n + " = " + number, res);
        Console.WriteLine(n + "√" + number + " = {0:N3}", res);
    }
}