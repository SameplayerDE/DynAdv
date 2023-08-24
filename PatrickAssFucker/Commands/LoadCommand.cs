using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    public class LoadCommand : ICommand
    {
        public void Execute(string[] args)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".assFucker", "saves");
            Console.WriteLine(folderPath);
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Speicherordner nicht gefunden.");
                return;
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
                    Console.WriteLine("Ungültiger Dateiname.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Ungültige Argumentanzahl.");
                return;
            }

            string filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                string loadedTime = File.ReadAllText(filePath);
                Console.WriteLine($"Gespeicherte Uhrzeit geladen: {loadedTime}");

                // Hier würde Ihre Logik zum Wiederherstellen des Spielzustands basierend auf den geladenen Daten eingefügt.
            }
            else
            {
                Console.WriteLine("Speicherdatei nicht gefunden.");
            }
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
