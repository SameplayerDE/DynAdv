using HxLocal;
using PatrickAssFucker.Areas;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    public class MoveCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                var currentPlayerArea = Brain.Instance.Player.CurrentArea;
                var linkedAreas = new List<Area>(currentPlayerArea.Linked);

                // Wenn sich der Spieler in einem Eingangsbereich befindet, fügen Sie die verbundenen Bereiche des übergeordneten Bereichs hinzu.
                if (currentPlayerArea.IsEntrance && currentPlayerArea.Parent != null)
                {
                    linkedAreas.AddRange(currentPlayerArea.Parent.Linked);
                }

                var selection = new SelectionPrompt<Area?>() // Beachten Sie das Fragezeichen, um Null-Werte zuzulassen.
                    .UseConverter(area => area?.Name ?? Localisation.GetString("common.cancel")) // "Abbrechen" anzeigen, wenn der Wert null ist.
                    .Title(Localisation.GetString("commands.move_selection_title"));

                foreach (var area in linkedAreas)
                {
                    if (area.CanSee())
                    {
                        selection.AddChoice(area);
                    }
                }

                // "Abbrechen"-Option hinzufügen
                selection.AddChoice(null);

                var chosenArea = AnsiConsole.Prompt(selection);

                if (chosenArea != null)
                {
                    Brain.Instance.Player.MoveTo(chosenArea);
                }
            }
            else
            {

            }
        }
    }
}
