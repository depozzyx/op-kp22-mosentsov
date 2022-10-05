using System;

public class Program
{
    public static void Main()
    {
        Task1();
        Task2();
        Task3();
    }

    const double Task1PriceBefore250 = 1.44;
    const double Task1PriceAfter250 = 1.68;
    public static void Task1()
    {
        Console.WriteLine("\n1. Відомо початкові та кінцеві показники спожитої електроенергії. Вартість 1 кВт·год електроенергії становить 1,44 грн, якщо обсяг споживання не перевищив 250 кВт·год. Для понаднормових витрат тариф становить 1,68грн. Обчисліть кількість спожитої електроенергії за та поза нормою, суму до сплати.");            
        Console.Write("Введіть кВт/год: ");
        double count = Convert.ToDouble(Console.ReadLine());
        double price = -1;

        if (count > 250)
        {
            price = 250 * Task1PriceBefore250 + (count - 250) * Task1PriceAfter250; 
        }
        else 
        {
            price = count * Task1PriceBefore250; 
        }

        Console.WriteLine("Ціна: " + price);
    }

    public static void Task2()
    {
        Console.WriteLine("\n2. За заданою температурою води потрібно визначити її агрегатний стан: «Твердий», «Рідкий» чи «Газоподібній».");
        Console.Write("Введіть температуру води: ");
        double temperature = Convert.ToDouble(Console.ReadLine());

        string waterState = "Рідкий";
        if (temperature >= 100)
        {
            waterState = "Газоподібний";
        }
        else if (temperature < 0) 
        {
            waterState = "Твердий";
        }

        Console.WriteLine("Стан води: " + waterState);
    }

    public static void Task3()
    {
        Console.WriteLine("\n3. Відомо номер місяця. Вивести на екран відповідну пору року.");           
        
        Console.Write("Введіть номер місяця: ");
        int month = Convert.ToInt16(Console.ReadLine());
        if (month <= 0 || month >= 13)
        {
            Console.WriteLine("Неправильний номер місяця");
            return;
        }

        string season = "";
        switch (month)
        {
            case 1:
            case 2:
            case 12:
                season = "Зима";
                break;

            case 3:
            case 4:
            case 5:
                season = "Весна";
                break;

            case 6:
            case 7:
            case 8:
                season = "Літо";
                break;

            case 9:
            case 10:
            case 11:
                season = "Осінь";
                break;
        }

        Console.WriteLine("Сезон: " + season);
    }

}

