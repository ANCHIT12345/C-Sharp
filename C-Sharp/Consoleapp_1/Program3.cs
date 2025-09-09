using System;
namespace MyApp3 {
    class Library {
        private readonly int LibraryOpeningYear;
        public Library(int openingYear) {
            LibraryOpeningYear = openingYear;
        }

        public void PrintMessage() {
            Console.WriteLine("Welcome to the Library!");
            Console.WriteLine($"Library opened in year: {LibraryOpeningYear}");
        }
    }

    public class Program3 {
        public static void Run() {
            Console.Write("Enter the year in which the library opend: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Library lib = new Library(1995);
            lib.PrintMessage();
            Console.ReadKey();
        }
    }
}
