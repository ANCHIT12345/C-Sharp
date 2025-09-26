using ConsoleApp4;
using System;
using System.ComponentModel;
namespace MyApp { 
    internal class Program {
        private static void Main(string[] args) {
            //IPayable payment1 = new CreditCardPayment();
            //payment1.ProcessPayment(100.00m);
            //IPayable payment2 = new UPIPayment();
            //payment2.ProcessPayment(200.00m);
            //Duck duck = new Duck();
            //duck.Fly();
            //duck.Swim();
            //IFlyable flyableDuck = duck;
            //ISwimmable swimmableDuck = duck;
            //flyableDuck.Fly();
            //swimmableDuck.Swim();
            //IDatabase db = new SqlDatabase();
            //db = new SqlDatabase();
            //db.Connect();
            //db.Disconnect();
            //Console.WriteLine();
            //db = new OracleDatabase();
            //db.Connect();
            //db.Disconnect();
            //CalculateBonus m = new Manager("Alice", 80000);
            //CalculateBonus d = new Developer("Bob", 60000);
            //m.DisplayBonus();
            //d.DisplayBonus();
            //Appliance vm = new WashingMachine("LG");
            //vm.TurnOn();
            //Appliance rf = new Refrigerator("Samsung");
            //rf.TurnOn();
            //Vehicle myCar = new car("Toyota");
            //myCar.Drive();
            //myCar.FuleType();
            //Vehicle myBike = new Bike("Yamaha");
            //myBike.Drive();
            //myBike.FuleType();
            //LibraryItem book = new Book("The Great Gatsby", "F. Scott Fitzgerald");
            //LibraryItem magazine = new Magazine("National Geographic", 202);
            //book.DisplayInfo();
            //magazine.DisplayInfo();
            //IReservable book2 = new Book2("1984", "George Orwell");
            //book2.Reserve();
            //IReservable dvd = new DVD("Inception", "Christopher Nolan");
            //dvd.Reserve();
            //Shape circle = new Circle("Red", 5.0);
            //Circle c = new Circle("Red", 5);
            //Rectangle r = new Rectangle("Blue", 4, 6);
            //c.Draw();
            //c.Resize(1.5);
            //c.Draw();
            //Console.WriteLine();
            //r.Draw();
            //r.Resize(2);
            //r.Draw();
            MultifunctionPrinter mfp = new MultifunctionPrinter();
            mfp.Print("MyDocument.pdf");
            mfp.Scan("MyDocument.pdf");
            mfp.Copy("MyDocument.pdf");
        }
    }
}