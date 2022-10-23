using System;

public class Program
{
    private static void writeSpaces(int number) 
    {
        for (int i = 0; i < number; i += 1)
        {
            Console.Write("  ");   
        }
    }
    private static int mathFactorial(int number) 
    {
        if (number == 0) return 1;
        if (number == 1) return number;
        // Console.WriteLine("n=" + number);
        // return number * (number - 1);   /
        return mathFactorial(number - 1) * number;
    }
    private static int mathCombinations(int k, int n) 
    {
        // Console.WriteLine("C(), n = " + n + " k = " + k);
        // some optimisations definitely needed
        return (mathFactorial(n)) / (mathFactorial(k) * mathFactorial(n - k));
    }

    public static void Main()
    {
        Console.Write("Введіть ціле число (>=1): ");
        int n = Convert.ToInt16(Console.ReadLine());
        for (int i = 0; i < n; i += 1)
        {
            int neededSpaces = n - i - 1;
            writeSpaces(neededSpaces);

            for (int j = 0; j < i + 1; j++)
            {
                int c = mathCombinations(j, i);
                if (c < 10) Console.Write(" ");
                Console.Write(c);
                Console.Write(" ");
                if (c < 100) Console.Write(" ");
            }

            writeSpaces(neededSpaces);
            Console.Write("\n");
        }
    }

}
//    1
//   1 1
//  1 2 1
// 1 3 3 1

// n = 4