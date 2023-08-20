using System;
using System.IO;
using System.Linq;
using HxLocal;
using Spectre.Console;

namespace PatrickAssFucker.Commands
{
    public class LanguageCommand : ICommand
    {
        private const string LanguageFolderPath = "Assets/"; // Pfad zu den Sprachdateien.

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                AnsiConsole.MarkupLine($"[green]Aktuelle Sprache:[/] {Localisation.CurrentLanguage}");
                ListAvailableLanguages();
            }
            else if (args.Length == 1)
            {
                SetLanguage(args[0]);
            }
        }

        private void ListAvailableLanguages()
        {
            // Listen Sie die Namen der JSON-Dateien im Verzeichnis auf.
            var availableLanguages = Directory.GetFiles(LanguageFolderPath, "*.json")
                                              .Select(Path.GetFileNameWithoutExtension)
                                              .ToArray();

            AnsiConsole.MarkupLine("\n[bold]Verfügbare Sprachen:[/]");
            foreach (var lang in availableLanguages)
            {
                AnsiConsole.MarkupLine($"- {lang}");
            }
        }

        private void SetLanguage(string langCode)
        {
            var langFile = Path.Combine(LanguageFolderPath, $"{langCode}.json");
            if (File.Exists(langFile))
            {
                Localisation.LoadLanguage(langCode);
                AnsiConsole.MarkupLine($"[green]Sprache erfolgreich auf {langCode} geändert![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Die Sprache '{langCode}' wird nicht unterstützt oder ist nicht verfügbar.[/]");
            }
        }
    }
}
