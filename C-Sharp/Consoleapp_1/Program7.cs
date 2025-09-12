using System;
namespace MyApp7;
class Program7
{
    public static void Run()
    {
        int num1 = 10;
        int num2 = num1;
        Console.WriteLine($"Before change: num1 = {num1}, num2 = {num2}");
        num2=20;
        Console.WriteLine($"After change: num1 = {num1}, num2 = {num2}");

        string str1 = "Hello";
        string str2 = str1;
        Console.WriteLine($"Before change: str1 = {str1}, str2 = {str2}");
        str2 = "World";
        Console.WriteLine($"After change: str1 = {str1}, str2 = {str2}");
        Console.WriteLine("Press Key TO exit");
        Console.ReadKey();
    }
}