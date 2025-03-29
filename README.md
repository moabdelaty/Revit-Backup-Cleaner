# Revit Backup Cleaner Tool

![KAITECH Logo](./KAITECH%20Logo.png)

## **Welcome to the Revit Backup Cleaner Tool**
This tool is designed to help you automate the cleaning of Revit backup files, freeing up space and keeping your directories organized. Whether you're working on projects or families, this tool ensures that unnecessary backups are removed efficiently.

---

## **Features**

- **Dry Run Mode**: Simulate the cleaning process without deleting files, ensuring no accidental removals.
- **Deep Cleaning**: Recursively clean all subdirectories to ensure no backups are left behind.
- **Custom Folder Input**: Specify folder paths via a `.txt` file for flexibility.
- **Interactive User Prompts**: Confirm your choices with simple yes/no inputs.
- **Detailed Notifications**: Receive summaries of the cleaning process via desktop notifications (Windows-only).
- **Space Recovery Report**: Know exactly how much storage was freed.

---

## **How It Works**

1. **Input Folder Paths**: Provide a `.txt` file containing the paths to directories you want to clean.
2. **Choose Preferences**:
   - Dry Run mode to preview deletions.
   - Deep Cleaning to include subdirectories.
3. **Run the Cleaner**: The tool scans the specified directories and deletes backup files with extensions `.????.rvt` and `.????.rfa`.
4. **Get Notifications**: Receive a summary of the cleaning process, including the number of files deleted and storage space recovered.

---

## **Installation and Usage**

### **Prerequisites**

- Windows OS
- .NET Framework 4.8 or higher
- [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/) package

### **Steps to Run**

1. Clone this repository:
   ```bash
   git clone https://github.com/moabdelaty/Revit-Backup-Cleaner.git
   cd Revit-Backup-Cleaner
   ```

2. Build the solution using Visual Studio (ensure you have the required .NET version).

3. Place a `.txt` file in the directory with paths to the folders you want to clean.

4. Run the executable from the command line:
   ```bash
   RevitBackupCleaner.exe
   ```

5. Follow the on-screen prompts to configure your cleaning preferences.

---

## **Usage Example**

1. Create a `.txt` file with the following content:
   ```txt
   C:\Projects\RevitFiles
   D:\Backup\Families
   ```

2. Run the tool and follow the prompts:
   ```
   Welcome to Revit Backup Cleaner Tool
   ====================================

   Enter the path to the .txt file containing folder paths:
   > paths.txt

   Do you want to enable Dry Run? (y/n): y
   Do you want to enable Deep Cleaning? (y/n): n
   ```

3. View the simulated deletions (Dry Run mode) or actual cleaning results.

---

## **Code Highlights**

### **Key Functions**

- **`RevitBackupCleaner()`**
  Handles user input, file reading, and invokes the cleaning process.

- **`FileCleaner()`**
  Deletes Revit backups based on user preferences, calculates total space recovered, and sends notifications.

- **`SizeFormated()`**
  Formats the size of deleted files into human-readable units (Bytes, KB, MB, GB).

- **`GetUserConfirmation()`**
  Simplifies yes/no user prompts.

---

## **Notifications**
The tool uses Windows Toast Notifications for desktop alerts. These notifications display:
- The number of files deleted
- Total space recovered
- An optional logo (if provided in the directory)

---

## **Screenshots**

### **Command-Line Interface**
![CLI Screenshot](./screenshots/cli-example.png)

### **Notification Example**
![Notification Example](./screenshots/notification-example.png)

---

## **Future Enhancements**

- Add support for non-Windows platforms.
- Implement a GUI for easier interaction.
- Allow more file types and custom extensions.
- Add logging for detailed reports.

---

## **Contributors**
Developed by **Mo Abdelaty**. If you have suggestions or encounter issues, feel free to open an [issue](https://github.com/moabdelaty/Revit-Backup-Cleaner/issues).

---

## **License**
This project is licensed under the [MIT License](./LICENSE).

---

Thank you for using the Revit Backup Cleaner Tool! 🚀

