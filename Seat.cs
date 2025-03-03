public class Seat
{
    public int Row { get; }
    public int Number { get; }
    public int Price { get; }
    public bool IsBooked { get; set; }

    public Seat(int row, int number, int price)
    {
        Row = row;
        Number = number;
        Price = price;
        IsBooked = false;
    }
}
