using HxLocal;
using PatrickAssFucker.Entities;
using Spectre.Console;

namespace PatrickAssFucker.Commands;

public class TalkCommand : ICommand
{
    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {

            var allEntities = Brain.Instance.Player.CurrentArea.Entities;
            
            // Filter out entities that implement the ITalkable interface.
            var talkableEntities = allEntities.OfType<ITalkable>().ToList();

            if (talkableEntities.Count == 0)
            {
                AnsiConsole.MarkupLine(Localisation.GetString("commands.talk_alone"));
                return;
            }

            var selection = new SelectionPrompt<Entity?>() // Beachten Sie das Fragezeichen, um Null-Werte zuzulassen.
                .UseConverter(entity => entity?.Name ?? Localisation.GetString("common.cancel")) // "Abbrechen" anzeigen, wenn der Wert null ist.
                .Title(Localisation.GetString("commands.talk_selection_title"));

            foreach (var entity in talkableEntities)
            {
                selection.AddChoice((Entity)entity);
            }

            // "Abbrechen"-Option hinzufügen
            selection.AddChoice(null);

            var option = AnsiConsole.Prompt(selection);

            if (option != null)
            {
                ((ITalkable)option).Talk();
                //option.Interact(); // If ITalkable has an Interact method, you can cast option to ITalkable and call Interact().
            }
        }
    }
}