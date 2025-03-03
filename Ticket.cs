public class Ticket
{
    public int Row { get; set; }
    public int Seat { get; set; }
    public int Price { get; set; }
    public Person Person { get; set; }

    public Ticket(int row, int seat, int price, Person person)
    {
        Row = row;
        Seat = seat;
        Price = price;
        Person = person;
    }

    public void Print()
    {
        Console.WriteLine("----Ticket Details----\n");
        Console.WriteLine($"Row: {Row}, Seat: {Seat}, Price: £{Price}");
        Person.Print();
    }
}
