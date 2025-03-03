using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

public class TicketBookingSystem
{
    private DSA.DynamicArray<Seat> seats = new();
    private DSA.LinkedList<Ticket> tickets = new DSA.LinkedList<Ticket>();
    public TicketBookingSystem()
    {
        for (int row = 1; row <= 3; row++)
        {
            int price = row == 1 ? 30 : row == 2 ? 20 : 10;
            for (int number = 1; number <= 20; number++)
            {
                seats.AddLast(new Seat(row, number, price));
            }
        }
    }

    public void DisplayMenu()
    {
        ShowPrice();
        PrintSeatingArea();
        while (true)
        {
            Console.WriteLine("\n------------------------------------------------\n");
            Console.WriteLine("Please select an option:\n");
            Console.WriteLine("1)  -> Buy a ticket");
            Console.WriteLine("2)  -> Cancel a booked ticket");
            Console.WriteLine("3)  -> Print the view of seating area");
            Console.WriteLine("4)  -> List of available seats");
            Console.WriteLine("5)  -> List of booked seats");
            Console.WriteLine("6)  -> Show person information of a booked seat");
            Console.WriteLine("7)  -> Calculate total price of booked tickets");
            Console.WriteLine("8)  -> Sort booked tickets by price");
            Console.WriteLine("9)  -> Sort booked tickets by name");
            Console.WriteLine("10) -> Sort booked tickets by age");
            Console.WriteLine("11) -> Sort booked tickets by email");
            Console.WriteLine("12)  -> Save booked tickets to file");
            Console.WriteLine("13) -> Load booked tickets from file");
            Console.WriteLine("00) -> Quit program");
            Console.WriteLine("\n------------------------------------------------\n");
            Console.Write("Enter option: ");

            if (int.TryParse(Console.ReadLine(),out int option))
            {
                if (option == 00)
                {
                    Console.WriteLine("Exiting program. Goodbye!\n");
                    break;
                }

                ExecuteOption(option);
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.\n");
            }
        }

    }
    private void ShowPrice()
    {
        Console.WriteLine("-----Prices Of Rows-----");
        Console.WriteLine("\n   -> First row  £30");
        Console.WriteLine("   -> Second row £20");
        Console.WriteLine("   -> Third row  £10\n");
    }

    private void ExecuteOption(int option)
    {
        switch (option)
        {
            case 1:
                BuyTicket();
                break;
            case 2:
                CancelTicket();
                break;
            case 3:
                PrintSeatingArea();
                break;
            case 4:
                ListAvailableSeats();
                break;
            case 5:
                ListBookedSeats();
                break;
            case 6:
                ShowPersonInfo();
                break;
            case 7:
                CalculateTotalPrice();
                break;
            case 8:
                SortTicketsByPrice();
                break;
            //case 9:
            //    SortTicketsByName();
            //    break;
            //case 10:
            //    SortTicketsByAge();
            //    break;
            //case 11:
            //    SortTicketsByEmail();
                break;
            case 12:
                SaveToFile();
                break;
            case 13:
                LoadFromFile();
                break;
            default:
                Console.WriteLine("Invalid option entered. Please try again.\n");
                break;
        }
        
    }

    private void BuyTicket()
    {
        Console.WriteLine();
        int row = GetValidRow();
        int seatNumber = GetValidSeatNumber();

        var seat = FindSeat(row, seatNumber);
        if (seat != null && !seat.IsBooked)
        {
            string name = GetValidName();

            int age = GetValidAge();

            string email = GetValidEmail();

            string date = GetValidDate();

            Person person = new Person(name, age, email, date);
            Ticket ticket = new Ticket(row, seatNumber, seat.Price, person);
            tickets.AddLast(ticket);

            seat.IsBooked = true;
            Console.WriteLine($"\nSeat {seatNumber} in Row {row} is booked successfully for £{seat.Price}.");
        }
        else
        {
            Console.WriteLine($"\nSeat {seatNumber} in Row {row} is already booked.");
        }
        Console.WriteLine();
    }

    private void CancelTicket()
    {
        int row = GetValidRow();
        int seatNumber = GetValidSeatNumber();

        var seat = FindSeat(row, seatNumber);
        if (seat != null && seat.IsBooked)
        {
            Ticket ticket = FindTicket(row, seatNumber);
            if (ticket != null)
            {
                Console.Write("Enter the email associated with the ticket: ");
                string email = Console.ReadLine();

                if (email == ticket.Person.Email)
                {
                    tickets.RemoveAt(tickets.Index(ticket));
                    Console.WriteLine($"Ticket for Seat {seatNumber} in Row {row} is canceled successfully.");
                    seat.IsBooked = false;
                }
                else
                {
                    Console.WriteLine("The entered email does not match the email associated with the ticket. Cannot cancel the ticket.");
                }
            }
            else
            {
                Console.WriteLine("Ticket not found for the given seat.");
            }
        }
        else
        {
            Console.WriteLine("Seat is not booked.");
        }
    }


    private void PrintSeatingArea()
    {
        Console.WriteLine();
        Console.WriteLine("              ****************");
        Console.WriteLine("              **   SCREEN   **");
        Console.WriteLine("              ****************\n");

        for (int row = 1; row <= 3; row++)
        {
            string leftSeats = "";
            string rightSeats = "";

            foreach (var seat in seats)
            {
                if (seat.Row == row)
                {
                    if (seat.Number <= 10)
                    {
                        leftSeats += seat.IsBooked ? " X" : " O";
                    }
                    else
                    {
                        rightSeats += seat.IsBooked ? " X" : " O";
                    }
                }
            }

            Console.WriteLine($"{leftSeats}  {rightSeats}");
        }
        Console.WriteLine();
    }

    private void ListAvailableSeats()
    {
        Console.WriteLine("\nAvailable Seats:");
        for (int row = 1; row <= 3; row++)
        {
            DSA.DynamicArray<int> availableSeats = new DSA.DynamicArray<int>();
            foreach (var seat in seats)
            {
                if (!seat.IsBooked && seat.Row == row)
                {
                    availableSeats.AddLast(seat.Number);
                }
            }

            if (availableSeats.Count > 0)
            {
                Console.WriteLine($"Row {row}:  {string.Join(", ", availableSeats)}");
            }
            else
            {
                Console.WriteLine($"Row {row}: No available seats. All seats are booked.");
            }
        }
        Console.WriteLine();
    }

    private void ListBookedSeats()
    {
        Console.WriteLine("\nBooked Seats:\n");
        for (int row = 1; row <= 3; row++)
        {
            DSA.DynamicArray<int> bookedSeats = new DSA.DynamicArray<int>();
            foreach (var seat in seats)
            {
                if (seat.IsBooked && seat.Row == row)
                {
                    bookedSeats.AddLast(seat.Number);
                }
            }

            if (bookedSeats.Count > 0)
            {
                Console.WriteLine($"Row {row}: {string.Join(", ", bookedSeats)}");
            }
            else
            {
                Console.WriteLine($"Row {row}: No seats are booked yet.");
            }
        }
        Console.WriteLine();
    }

    private void ShowPersonInfo()
    {
        int row = GetValidRow();
        int seatNumber = GetValidSeatNumber();

        Ticket ticket = FindTicket(row, seatNumber);
        if (ticket != null)
        {
            ticket.Person.Print();
        }
        else
        {
            Console.WriteLine("No ticket found for the given seat.");
        }
    }

    private void CalculateTotalPrice()
    {
        int total = 0;
        int[] rowCount = new int[3]; // To store the count of sold tickets for each row

        foreach (var ticket in tickets)
        {
            total += ticket.Price;
            rowCount[ticket.Row - 1]++; // Increment the count for the respective row
        }

        // Display the number of tickets sold for each row
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                Console.WriteLine($"Tickets sold from row {i + 1}: {rowCount[i]}*£30");
            }
            else if (i == 1){
                Console.WriteLine($"Tickets sold from row {i + 1}: {rowCount[i]}*£20");
            }
            else{
                Console.WriteLine($"Tickets sold from row {i + 1}: {rowCount[i]}*£10");
            }
        }

        // Display the total price
        Console.WriteLine($"Total price of all sold tickets: £{total}\n");
    }


    private void SortTicketsByPrice()
    {
        var ticketArray = new DSA.DynamicArray<Ticket>(tickets);

        for (int i = 0; i < ticketArray.Count - 1; i++)
        {
            for (int j = 0; j < ticketArray.Count - i - 1; j++)
            {
                if (ticketArray.At(j).Price > ticketArray.At(j + 1).Price)
                {
                    var temp = ticketArray[j];
                    ticketArray[j] = ticketArray[j + 1];
                    ticketArray[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("\nTickets sorted by price:\n");
        foreach (var ticket in ticketArray)
        {
            ticket.Print();
            Console.WriteLine();
        }
    }

    private void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter("C:/Users/nethm/Desktop/dsa project/.vs/tickets.txt"))
        {
            foreach (var ticket in tickets)
            {
                writer.WriteLine($"{ticket.Row},{ticket.Seat},{ticket.Price},{ticket.Person.Name},{ticket.Person.Age},{ticket.Person.Email},{ticket.Person.Date}");
            }
        }
        Console.WriteLine("Tickets saved to file successfully.\n");
    }

    private void LoadFromFile()
    {
        if (!File.Exists("tickets.txt"))
        {
            Console.WriteLine("No saved tickets file found.\n");
            return;
        }

        using (StreamReader reader = new StreamReader("C:/Users/nethm/Desktop/dsa project/.vs/tickets.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(',');
                int row = int.Parse(data[0]);
                int seat = int.Parse(data[1]);
                int price = int.Parse(data[2]);
                string name = data[3];
                int age = int.Parse(data[4]);
                string email = data[5];
                string date = data[6];

                Person person = new Person(name, age, email, date);
                Ticket ticket = new Ticket(row, seat, price, person);
                tickets.AddLast(ticket);

                var seatObj = FindSeat(row, seat);
                if (seatObj != null)
                {
                    seatObj.IsBooked = true;
                }
            }
        }
        Console.WriteLine("Tickets loaded from file successfully.\n");
    }

    private Seat FindSeat(int row, int number)
    {
        foreach (var seat in seats)
        {
            if (seat.Row == row && seat.Number == number)
            {
                return seat;
            }
        }
        return null;
    }

    private Ticket FindTicket(int row, int seat)
    {
        foreach (var ticket in tickets)
        {
            if (ticket.Row == row && ticket.Seat == seat)
            {
                return ticket;
            }
        }
        return null;
    }

    private string GetValidName()
    {
        while (true)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name) && name.Length >= 3 && ContainsAtLeastThreeLetters(name))
            {
                return name;
            }
            else
            {
                Console.WriteLine("Invalid name. Please enter a name with at least 3 letters.\n");
            }
        }
    }

    private bool ContainsAtLeastThreeLetters(string name)
    {
        int letterCount = 0;

        foreach (char c in name)
        {
            if (char.IsLetter(c))
            {
                letterCount++;
                if (letterCount >= 3)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private int GetValidRow()
    {
        int row;
        while (true)
        {
            Console.Write("Enter Row Number (1-3): ");
            if (int.TryParse(Console.ReadLine(), out row) && row >= 1 && row <= 3)
            {
                return row;
            }
            else
            {
                Console.WriteLine("Invalid row number. Please enter a number between 1 and 3.\n");
            }
        }
    }

    private int GetValidSeatNumber()
    {
        int number;
        while (true)
        {
            Console.Write("Enter Seat Number (1-20): ");
            if (int.TryParse(Console.ReadLine(), out number) && number >= 1 && number <= 20)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid seat number. Please enter a number between 1 and 20.\n");
            }
        }
    }

    private int GetValidAge()
    {
        int age;
        while (true)
        {
            Console.Write("Enter Age: ");
            string input = Console.ReadLine();

            // Check if the input is a valid integer
            if (!int.TryParse(input, out age))
            {
                Console.WriteLine("Invalid input. Please enter numbers.\n");
                continue;
            }

            // Check if the age is positive
            if (age > 0)
            {
                return age;
            }
            else
            {
                Console.WriteLine("Invalid age. Please enter a positive number.\n");
            }
        }
    }


    private string GetValidEmail()
    {
        string email;
        while (true)
        {
            Console.Write("Enter Email: ");
            email = Console.ReadLine();
            if (email.Contains("@") && email.Contains("."))
            {
                return email;
            }
            else
            {
                Console.WriteLine("Invalid email format. Please try again.\n");
            }
        }
    }

    private string GetValidDate()
    {
        string date;
        while (true)
        {
            Console.Write("Enter Booking Date (yyyy-mm-dd): ");
            date = Console.ReadLine();

            if (DateTime.TryParse(date, out _))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.\n");
            }
        }
    }
}
