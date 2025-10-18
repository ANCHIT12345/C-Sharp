using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileHandling
{
    class CopyMove
    {
        public void Run()
        {
            string sourceFile = "users.txt";
            string backupFile = "backup_users.txt";
            string backupFolder = "Backup";
            string logFile = "filelog.txt";

            try
            {
                if (!File.Exists(sourceFile))
                {
                    Console.WriteLine("Error: users.txt file not found!");
                    return;
                }
                File.Copy(sourceFile, backupFile, true);
                Console.WriteLine("File copied successfully as backup_users.txt");
                if (!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(backupFolder);
                    Console.WriteLine("Backup folder created.");
                }
                string destinationPath = Path.Combine(backupFolder, backupFile);
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }
                File.Move(backupFile, destinationPath);
                Console.WriteLine("Backup file moved to Backup folder successfully!");
            }
            catch (IOException ex)
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine($"{DateTime.Now}: File operation error - {ex.Message}");
                }
                Console.WriteLine("File operation error: " + ex.Message);
            }
            catch (Exception ex)
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine($"{DateTime.Now}: File operation error - {ex.Message}");
                }
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }
    }
}
