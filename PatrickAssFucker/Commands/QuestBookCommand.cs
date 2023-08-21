using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    public class QuestBookCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {

                //if (!Brain.Instance.Player.QuestBook.ActiveQuests.Any())
                //{
                //    AnsiConsole.MarkupLine("[yellow]You have no active quests![/]");
                //}
                //else
                //{
                //    var selectionPrompt = new SelectionPrompt<string>();
                //    selectionPrompt.Title("Welchen Auftrag möchtest du genauer betrachten?");
                //    selectionPrompt.PageSize(10);
                //    foreach (var quest in Brain.Instance.Player.QuestBook.ActiveQuests)
                //    {
                //        selectionPrompt.AddChoice(quest.Title + $"{quest.CompletionPercentage():0.00}%");
                //    }
                //    var fruit = AnsiConsole.Prompt(selectionPrompt);
                //
                //    // Echo the fruit back to the terminal
                //    AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
                //
                //    
                //}
                //DisplayQuests();

                // Create the layout
                var layout = new Layout("Root")
                    .SplitColumns(
                        new Layout("Left"),
                        new Layout("Right")
                            .SplitRows(
                                new Layout("Top"),
                                new Layout("Bottom")));

                // Update the left column
                layout["Left"].Update(
                    new Panel(
                        Align.Center(
                            new Markup("Hello [blue]World![/]"),
                            VerticalAlignment.Middle))
                        .Expand());

                // Render the layout
                AnsiConsole.Write(layout);


            }
        }

        private void DisplayQuests()
        {
            /*var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[yellow]Quest[/]").Centered())
                .AddColumn(new TableColumn("[yellow]Completion[/]").Centered());

            foreach (var quest in Brain.Instance.Player.QuestBook.ActiveQuests)
            {
                table.AddRow(quest.Title, $"{quest.CompletionPercentage():0.00}%");
            }

            if (!Brain.Instance.Player.QuestBook.ActiveQuests.Any())
            {
                AnsiConsole.MarkupLine("[yellow]You have no active quests![/]");
            }
            else
            {
                AnsiConsole.Write(table);
            }*/
        }
    }
}
