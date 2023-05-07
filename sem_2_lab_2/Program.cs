using System;
using System.Collections.Generic;

class Program 
{
    static void Main(String[] args) 
    {
        Task1();
        Task21();
        Task22();
        Task23();
        Task3();
    }

    static void Task1()
    {
        var n = (uint) Utils.readNumber("кількість записів таблиці");
        var records = new List<Record>();

        for (int i = 0; i < n; i++)
        {
            records.Add(new Record((uint) i + 1, "name", 100, 120));
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
        var records = new Records();
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

}

class Utils 
{
    public static double readNumber(string variableName)
    {
        Console.Write("Введіть " + variableName + " : ");
        return Convert.ToDouble(Console.ReadLine());
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
            records.Add(new Record((uint) i + 1, "name", 100, 120));
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
