using System;
using System.Collections.Generic;

class Program 
{
    static void Main(String[] args) 
    {
        Console.WriteLine("\n\nЗавдання #1");
        Task1();

        Console.WriteLine("\n\nЗавдання #2.1");
        Task21();

        Console.WriteLine("\n\nЗавдання #2.2");
        Task22();

        Console.WriteLine("\n\nЗавдання #2.3");
        Task23();

        Console.WriteLine("\n\nЗавдання #3");
        Task3();
    }

    static void Task1()
    {
        var n = (uint) Utils.readNumber("кількість записів таблиці");
        var records = new List<Record>();

        for (int i = 0; i < n; i++)
        {
            var name = Utils.readString("Ім'я #" + (i + 1));
            var salary = (uint) Utils.readNumber("Зарплату грн #" + (i + 1));
            var on_hold = (uint) Utils.readNumber("Утримано грн #" + (i + 1));
            records.Add(new Record((uint) i + 1, name, salary, on_hold));
        }

        uint totalSalary = 0, totalOnHold = 0, totalReceived = 0;
        Utils.printTableRow(new string[] {"№ з/п", "Прізвище", "Зарплата грн", "Утримано грн", "Видано грн"});
        for (int i = 0; i < n; i++)
        {
            Utils.printTableRow(new string[] {
                records[i].index.ToString(), 
                records[i].name, 
                records[i].salary.ToString(), 
                records[i].onHold.ToString(), 
                records[i].received.ToString()
            });

            totalSalary += records[i].salary;
            totalOnHold += records[i].onHold;
            totalReceived += records[i].received;
        }
        Utils.printTableRow(new string[] {
            "Σ", 
            "Разом", 
            totalSalary.ToString(), 
            totalOnHold.ToString(), 
            totalReceived.ToString()
        });
    }

    static void Task21()
    {
        new Records();
    }

    static void Task22()
    {
        var vessels = new Vessel[] {new SailingVessel(), new Submarine(), new Submarine()};

        foreach (var vessel in vessels)
        {
            vessel.PrepareToMovement();
            vessel.Move();
        }
    }

    static void Task23()
    {
        var vessels = new IVessel[] {new Submarine2(), new SailingVessel2()};

        foreach (var vessel in vessels)
        {
            vessel.PrepareToMovement();
            vessel.Move();
        }
    }

    static void Task3()
    {
        Vector vector1 = new Vector(new int[] { -1, 2, -3, 4, -5 });
        Vector vector2 = new Vector(new int[] { 1, -2, 3, -4, 5 });

        Console.WriteLine("Два вектора: " + vector1 + " & " + vector2);

        int sum = vector1 + vector2;
        Console.WriteLine("Сума від'ємних елементів двох векторів: " + sum);

        Vector mult = vector1 * vector2;
        Console.WriteLine("Добуток елементів двох векторів із парними номерами: " + mult);

        int zeroCount = vector1 == vector2;
        Console.WriteLine("Кількість елементів двох векторів, рівних 0: " + zeroCount);
    }
}

class Utils 
{
    public static double readNumber(string variableName)
    {
        Console.Write("Введіть " + variableName + " : ");
        return Convert.ToDouble(Console.ReadLine());
    }

    public static string readString(string variableName)
    {
        Console.Write("Введіть " + variableName + " : ");
        return Console.ReadLine();
    }

    public static void printFixed(string str, uint fixedn)
    {
        Console.Write(str);
        for (int i = 0; i < fixedn - str.Length; i++)
            Console.Write(" ");   
    }

    public static void printTableRow(string[] table)
    {
        Console.Write(" ");
        for (int i = 0; i < table.Length; i++)
        {
            if (i != 0) 
                Console.Write(" | ");

            printFixed(table[i], 14);
        }
        Console.Write(" \n");
    }
}

// Task 1
class Record
{
    public uint index {get;}
    public string name {get;}
    public uint salary {get;}
    public uint onHold {get;}

    public uint received 
    {
        get { return this.salary - this.onHold; }
    }
    
    public Record(uint index, string name, uint salary, uint onHold)
    {
        this.index = index;
        this.name = name;
        this.salary = salary;
        this.onHold = onHold;
    }
}

// Task 2.1
class Records
{
    public List<Record> records {get;}

    public Records()
    {
        records = new List<Record>();

        var n = (uint) Utils.readNumber("кількість записів таблиці");
        for (int i = 0; i < n; i++)
        {
            var name = Utils.readString("Ім'я #" + (i + 1));
            var salary = (uint) Utils.readNumber("Зарплату грн #" + (i + 1));
            var on_hold = (uint) Utils.readNumber("Утримано грн #" + (i + 1));
            records.Add(new Record((uint) i + 1, name, salary, on_hold));
        }

        uint totalSalary = 0, totalOnHold = 0, totalReceived = 0;
        Utils.printTableRow(new string[] {"№ з/п", "Прізвище", "Зарплата грн", "Утримано грн", "Видано грн"});
        for (int i = 0; i < n; i++)
        {
            Utils.printTableRow(new string[] {
                records[i].index.ToString(), 
                records[i].name, 
                records[i].salary.ToString(), 
                records[i].onHold.ToString(), 
                records[i].received.ToString()
            });

            totalSalary += records[i].salary;
            totalOnHold += records[i].onHold;
            totalReceived += records[i].received;
        }
        Utils.printTableRow(new string[] {
            "Σ", 
            "Разом", 
            totalSalary.ToString(), 
            totalOnHold.ToString(), 
            totalReceived.ToString()
        });
    }
}

// Task 2.2
abstract class Vessel 
{
    public abstract void PrepareToMovement();
    public abstract void Move();
}

class SailingVessel : Vessel 
{
    public override void PrepareToMovement()
    {
        Console.WriteLine("SailingVessel is prepearing to move");
    }
    public override void Move()
    {
        Console.WriteLine("SailingVessel moving");
    }
}

class Submarine : Vessel 
{
    public override void PrepareToMovement()
    {
        Console.WriteLine("Submarine is prepearing to move");
    }
    public override void Move()
    {
        Console.WriteLine("brrrrrr..., Submarine moving");
    }
}

// Task 2.3
interface IVessel
{
    void PrepareToMovement();
    void Move();
}

class SailingVessel2 : IVessel 
{
    public void PrepareToMovement()
    {
        Console.WriteLine("interfaced SailingVessel is prepearing to move");
    }
    public void Move()
    {
        Console.WriteLine("SailingVessel moving in interface");
    }
}

class Submarine2 : IVessel 
{
    public void PrepareToMovement()
    {
        Console.WriteLine("Interface Submarine is prepearing to move");
    }
    public void Move()
    {
        Console.WriteLine("*intface brrrrrr...*, Submarine moving");
    }
}

// Task 3
class Vector
{
    private int[] elements;

    public Vector(int[] elements)
    {
        this.elements = elements;
    }

    public static int operator +(Vector vector1, Vector vector2)
    {
        int count = 0;

        for (int i = 0; i < vector1.elements.Length; i++)
            if (vector1.elements[i] < 0)
                count += vector1.elements[i];

        for (int i = 0; i < vector2.elements.Length; i++)
            if (vector2.elements[i] < 0)
                count += vector2.elements[i];

        return count;
    }

    public static Vector operator *(Vector vector1, Vector vector2)
    {
        int len = vector1.elements.Length;
        int[] result = new int[len % 2 == 0 ? (int) len / 2 : (int) len / 2 + 1];
        int index = 0;
        for (int i = 0; i < vector1.elements.Length; i += 2) 
        {
            result[index] = vector1.elements[index] * vector2.elements[index];
            index += 1;
        }

        return new Vector(result);
    }

    public static int operator ==(Vector vector1, Vector vector2)
    {
        int count = 0;
        for (int i = 0; i < vector1.elements.Length; i++)
            if (vector1.elements[i] == 0)
                count += 1;

        for (int i = 0; i < vector2.elements.Length; i++)
            if (vector2.elements[i] == 0)
                count += 1;

        return count;
    }

    public static int operator !=(Vector vector1, Vector vector2)
    {
        return -1;
    }

    public override string ToString()
    {
        return "Vector<" + string.Join(", ", elements) + ">";
    }
}
