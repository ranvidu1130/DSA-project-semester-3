using System;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Date { get; set; }

    public Person(string name, int age, string email, string date)
    {
        Name = name;
        Age = age;
        Email = email;
        Date = date;
    }

    public void Print()
    {
        //Console.WriteLine("\n----Person Details----\n");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Date: {Date}");
    }

    public void Print1()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Date: {Date}");
    }
}

