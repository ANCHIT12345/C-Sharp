using System;
namespace MyApp6;
class Program6
{
    public static void Run()
    {
        int x = Convert.ToInt32(Console.ReadLine());
        int? Fine_Ammount = null;
        if (Fine_Ammount.HasValue)
            Console.WriteLine($"Total fine is {Fine_Ammount}");
        else
            Console.WriteLine("No fine due");
        Console.ReadKey();
    }
}