using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileHandling;

namespace SampleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //StreamWriter();
            //StreamWriterAppend();
            //StreamReader();
            //filecheck.Run();
            //CopyMove copyMove = new CopyMove();
            //copyMove.Run();
            //DeleteFile();
            //FileStats();
            //SearchFile();
            JSONSerialization.Run();
            Console.Read();
        }
        public static void StreamWriter()
        {
            Console.WriteLine("Enter Your Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Your Email:");
            string email = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("User.txt", true))
            {
                writer.WriteLine($"Name: {name}");
                writer.WriteLine($"Email: {email}");
                writer.WriteLine("---------------------");
            }
        }
        public static void StreamReader()
        {
            using StreamReader reader = new StreamReader("User.txt");
            string content = reader.ReadToEnd();
            Console.WriteLine("---------------------");
            Console.WriteLine(content);
        }
        public static void StreamWriterAppend()
        {
            Console.WriteLine("Enter Your Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Your Email:");
            string email = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter("User.txt", append: true))
            {
                writer.WriteLine($"Name: {name}");
                writer.WriteLine($"Email: {email}");
                writer.WriteLine("---------------------");
            }
        }
        public static void DeleteFile()
        {
            Console.WriteLine("Deleting User.txt file...");
            Console.WriteLine("Options: 1. Delete File, 2. Cancel");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                if (File.Exists("User.txt"))
                {
                    File.Delete("User.txt");
                    Console.WriteLine("User.txt file deleted successfully.");
                }
                else
                {
                    Console.WriteLine("User.txt file does not exist.");
                }
            }
            else
            {
                Console.WriteLine("File deletion cancelled.");
            }
        }
        public static void FileStats()
        {
            string filePath = "User.txt";
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File does not exist.");
                    return;
                }
                CountFileDetails(filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
        static void CountFileDetails(string filePath)
        {
            int lineCount = 0;
            int wordCount = 0;
            int charCount = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    charCount += line.Length;
                    wordCount += line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
            Console.WriteLine($"File: {filePath}");
            Console.WriteLine($"Lines: {lineCount}");
            Console.WriteLine($"Words: {wordCount}");
            Console.WriteLine($"Characters: {charCount}");
        }
        public static void SearchFile()
        {
            string filePath = "User.txt";
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File does not exist.");
                    return;
                }
                Console.WriteLine("Enter the word to search:");
                string searchWord = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(searchWord))
                {
                    Console.WriteLine("Invalid input. Please enter a valid word.");
                    return;
                }
                int lineNumber = 0;
                bool found = false;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (line.Contains(searchWord, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Found '{searchWord}' on line {lineNumber}: {line}");
                            found = true;
                        }
                    }
                }
                if (!found)
                {
                    Console.WriteLine($"The word '{searchWord}' was not found in the file.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}