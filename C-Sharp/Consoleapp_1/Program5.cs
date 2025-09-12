using System;
namespace MyApp5;

public class Program5
{
    public static void Run()
    {
        Console.WriteLine("Enter the name of the Student:");
        string name = Console.ReadLine();
        Console.WriteLine("Enter the roll_on of the Student:");
        int roll_on = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Gender of the Student:");
        string gender = Console.ReadLine();
        Console.WriteLine("Has library card (yes,no): ");
        string Cardcheck = Console.ReadLine();
        bool HasLibraryCard = Cardcheck.Equals("yes", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine($"Name of the student is {name}");
        Console.WriteLine($"Roll on of the student is {roll_on}");
        Console.WriteLine($"Gender of the student is {gender}");
        Console.WriteLine($"has card: {HasLibraryCard}");
        Console.ReadKey();
    }
}