using System;
using System.IO;
using System.Linq;
using HxLocal;
using Spectre.Console;

namespace PatrickAssFucker.Commands
{
    public class LanguageCommand : ICommand
    {

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                AnsiConsole.MarkupLine($"[green]Aktuelle Sprache:[/] {Localisation.CurrentLanguage}");
                ListAvailableLanguages();
            }
            else if (args.Length == 1)
            {
                if (args[0] == "reload")
                {
                    try
                    {
                        Localisation.Reload();
                        AnsiConsole.MarkupLine("[green]Aktuelle Sprache wurde erfolgreich neu geladen![/]");
                    }
                    catch (LocalisationException ex)
                    {
                        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                    }
                    return;
                }
                SetLanguage(args[0]);
            }
        }

        private void ListAvailableLanguages()
        {
            var availableLanguages = Localisation.ListAvailableLanguages();

            AnsiConsole.MarkupLine("\n[bold]Verfügbare Sprachen:[/]");
            foreach (var lang in availableLanguages)
            {
                AnsiConsole.MarkupLine($"- {lang}");
            }
        }

        private void SetLanguage(string langCode)
        {
            try
            {
                Localisation.LoadLanguage(langCode);
                AnsiConsole.MarkupLine($"[green]Sprache erfolgreich auf {langCode} geändert![/]");
            }
            catch (LanguageNotFoundException)
            {
                AnsiConsole.MarkupLine($"[red]Die Sprache '{langCode}' wird nicht unterstützt oder ist nicht verfügbar.[/]");
            }
            catch (JsonParseException ex)
            {
                AnsiConsole.MarkupLine($"[red]Fehler beim Parsen der JSON-Inhalte für {langCode}: {ex.Message}[/]");
            }
            catch (IOOperationException ex)
            {
                AnsiConsole.MarkupLine($"[red]I/O-Fehler für {langCode}: {ex.Message}[/]");
            }
        }
    }
}
