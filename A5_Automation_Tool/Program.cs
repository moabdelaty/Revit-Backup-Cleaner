using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Xml.XPath;
using System.Runtime.CompilerServices;

namespace A5_Automation_Tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Revit Backup Cleaner Tool");
            Console.WriteLine("====================================\n");

            try
            {
                RevitBackupCleaner();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nCleaning process completed. Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Remove Revit backups (projects or families) in a specific folder. Also shows notification.
        /// </summary>
        static void RevitBackupCleaner()
        {
            // Get Revit folder paths from the txt file
            Console.WriteLine("Enter the path to the .txt file containing folder paths:");
            string pathFile = Console.ReadLine();

            if (string.IsNullOrEmpty(pathFile) || !File.Exists(pathFile))
            {
                Console.WriteLine("Invalid file path. Please ensure the file exists.");
                return;
            }

            string[] paths = File.ReadAllLines(pathFile);

            // Define the extensions to clean
            string[] extensions = { "rvt", "rfa" };

            // Get user preferences for Dry Run and Deep Cleaning
            bool dryRun = GetUserConfirmation("Do you want to enable Dry Run? (y/n): ");
            bool deepCleaning = GetUserConfirmation("Do you want to enable Deep Cleaning? (y/n): ");

            FileCleaner(paths, extensions, dryRun, deepCleaning);
        }

        static void FileCleaner(string[] paths, string[] extensions, bool dryRun = false, bool deepCleaning = false)
        {
            ToastContentBuilder notif = new ToastContentBuilder();

            long totalSize = 0;
            int deletedFilesCount = 0;

            foreach (var path in paths)
            {
                DirectoryInfo di = new DirectoryInfo(path);

                if (!di.Exists)
                {
                    Console.WriteLine($"The specified path \"{path}\" does not exist.");
                    continue;
                }

                foreach (var extension in extensions)
                {
                    var files = di.GetFiles($"*.????.{extension}",
                        deepCleaning ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                    foreach (FileInfo file in files)
                    {
                        if (dryRun)
                        {
                            Console.WriteLine($"Dry Run: The program would delete {file.Name}");
                        }
                        else
                        {
                            try
                            {
                                file.Delete();
                                totalSize += file.Length;
                                deletedFilesCount++;
                                Console.WriteLine($"Deleted: {file.FullName}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting {file.FullName}: {ex.Message}");
                            }
                        }
                    }
                }
            }

            if (!dryRun)
            {
                notif.AddText("Revit Backup Cleaner Completed");
                notif.AddText($"{deletedFilesCount} files cleaned (projects and families), freeing {SizeFormated(totalSize)}.");

                string logoPath = @".\KAITECH Logo.png";
                if (File.Exists(logoPath))
                {
                    notif.AddInlineImage(new Uri(Path.GetFullPath(logoPath)));
                }
                else
                {
                    Console.WriteLine("Notification logo not found.");
                }

                notif.Show();
                Console.WriteLine($"\nSummary: Deleted {deletedFilesCount} file(s), freeing {SizeFormated(totalSize)}.");
            }
        }

        static bool GetUserConfirmation(string message)
        {
            Console.WriteLine(message);
            string response = Console.ReadLine();
            return !string.IsNullOrEmpty(response) && response.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        static string SizeFormated(long totalSize)
        {
            const long KB = 1024;
            const long MB = KB * 1024;
            const long GB = MB * 1024;

            if (totalSize >= GB)
                return $"{totalSize / (double)GB:0.## GB}";
            if (totalSize >= MB)
                return $"{totalSize / (double)MB:0.## MB}";
            if (totalSize >= KB)
                return $"{totalSize / (double)KB:0.## KB}";

            return $"{totalSize} Bytes";
        }
    }
}
