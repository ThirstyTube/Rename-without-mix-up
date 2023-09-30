using System;
using System.IO;

class FileRenameTool
{
    static void Main()
    {
        Console.WriteLine("File Rename Tool");
        Console.WriteLine("----------------");

        while (true)
        {
            // Get the directory path from the user
            Console.Write("Enter the directory path (or 'exit' to quit): ");
            string directoryPath = Console.ReadLine();

            if (directoryPath.ToLower() == "exit")
                break;

            // Get the file extension from the user
            Console.Write("Enter the file extension (e.g., .txt): ");
            string fileExtension = Console.ReadLine();

            // Get the new name prefix from the user
            Console.Write("Enter the new name prefix: ");
            string newNamePrefix = Console.ReadLine();

            // Get the starting number for renaming
            Console.Write("Enter the starting number for renaming: ");
            int startingNumber;
            if (!int.TryParse(Console.ReadLine(), out startingNumber))
            {
                Console.WriteLine("Invalid starting number. Try again.");
                continue;
            }

            try
            {
                // Get the files in the specified directory with the given extension
                string[] files = Directory.GetFiles(directoryPath, "*" + fileExtension);

                int counter = startingNumber;
                int padding = Math.Max(1, files.Length.ToString().Length); // Determine the padding length for numbering

                foreach (string filePath in files)
                {
                    // Get the file name without the path
                    string fileName = Path.GetFileName(filePath);

                    // Generate the new file name with the incremented number
                    string newFileName = $"{newNamePrefix}{counter.ToString().PadLeft(padding, '0')}{Path.GetExtension(fileName)}";

                    // Get the new file path
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    // Rename the file
                    File.Move(filePath, newFilePath);

                    Console.WriteLine($"Renamed: {filePath} -> {newFilePath}");

                    counter++;
                }

                Console.WriteLine("All files renamed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
