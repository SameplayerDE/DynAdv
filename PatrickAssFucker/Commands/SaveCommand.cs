using Newtonsoft.Json;
using PatrickAssFucker.Managers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    public class SaveCommand : ICommand
    {
        public void Execute(string[] args)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".assFucker", "saves");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName;
            if (args.Length == 0)
            {
                fileName = "default.save";
            }
            else if (args.Length == 1)
            {
                fileName = CleanFileName(args[0]) + ".save";

                if (string.IsNullOrWhiteSpace(fileName) || IsReservedName(Path.GetFileNameWithoutExtension(fileName)))
                {
                    AnsiConsole.WriteLine("Ungültiger Dateiname.");
                    return;
                }
            }
            else
            {
                AnsiConsole.WriteLine("Ungültige Argumentanzahl.");
                return;
            }

            string filePath = Path.Combine(folderPath, fileName);
            string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); // Formatieren Sie das aktuelle Datum und die Uhrzeit

            string jsonString = JsonConvert.SerializeObject(StoryProgress.Instance, Formatting.Indented);
            File.WriteAllText(filePath, jsonString); // Schreibt die aktuelle Uhrzeit in die Datei
        }

        private string CleanFileName(string name)
        {
            // Entfernen Sie ungültige Zeichen aus dem Dateinamen
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
        }

        private bool IsReservedName(string name)
        {
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

            return reservedNames.Contains(name.ToUpper());
        }
    }
}
