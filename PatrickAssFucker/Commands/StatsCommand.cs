using HxLocal;
using Spectre.Console;
using System.Xml.Linq;

namespace PatrickAssFucker.Commands
{
    public class StatsCommand : ICommand
    {
        private Tuple<Color, Color> CalculateProgressBarColors(double percentage)
        {
            if (percentage > 0.5)
                return Tuple.Create(Color.Green, Color.DarkGreen);
            else if (percentage > 0.2)
                return Tuple.Create(Color.Orange1, Color.Orange4_1);
            else
                return Tuple.Create(Color.Red, Color.DarkRed);
        }

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                var table = new Table();

                table.AddColumn(new TableColumn(new Markup("[yellow]Bezeichung[/]")));
                table.AddColumn(new TableColumn(new Markup("[yellow]Wert[/]")));
                
                table.AddRow(Localisation.GetString("current_area_root"), "" + Brain.Instance.Player.CurrentArea.GetTopParentName());
                table.AddRow(Localisation.GetString("current_area_node"), "" + Brain.Instance.Player.CurrentArea.Name);

                table.AddEmptyRow();
                
                //table.AddRow(Localisation.GetString("in_hand"), (Brain.Instance.Player.Equipped.Material == Material.NONE) ? "Kein Item" : Brain.Instance.Player.Equipped.Name);

                table.AddEmptyRow();

                //table.AddRow(Localisation.GetString("maximum_health"), "" + Brain.Instance.Player.MaxHealth);
                //table.AddRow(Localisation.GetString("current_health"), "" + Brain.Instance.Player.Health);
                
                table.AddEmptyRow();

                table.AddRow(Localisation.GetString("maximum_mana"), "");
                table.AddRow(Localisation.GetString("current_mana"), "");

                table.AddEmptyRow();

                //table.AddRow(Localisation.GetString("current_exp"), "" + (Brain.Instance.Player.Exp));
                //table.AddRow(Localisation.GetString("current_level"), "" + (Brain.Instance.Player.Level));
                //table.AddRow(Localisation.GetString("exp_to_level_up"), "" + (Brain.Instance.Player.ExpNeededForLevel(Brain.Instance.Player.Level + 1) - Brain.Instance.Player.Exp));

                table.AddEmptyRow();

                //table.AddRow("akt. Gold", "");
                //table.AddRow("anz. Gegentstände im Beutel", "");

                AnsiConsole.Write(table);
            }
            /**else if (args.Length == 1)
            {
                switch (args[0].ToLower())
                {
                    case "hand":
                        AnsiConsole.MarkupLine("[yellow]Du trägst[/] [gold1]" + ((Brain.Instance.Player.Equipped.Material == Material.NONE) ? "kein Item" : Brain.Instance.Player.Equipped.Name) + "[/] [yellow]in der Hand.[/]");
                        return;

                    case "leben":
                        ShowProgress(Color.Lime, Color.DarkGreen, Brain.Instance.Player.MaxHealth, 0, Brain.Instance.Player.Health, true, true);
                        return;

                    case "stufe":
                        ShowProgress(Color.DeepPink1, Color.DeepPink4, Brain.Instance.Player.ExpNeededForLevel(Brain.Instance.Player.Level + 1), 0, Brain.Instance.Player.Exp, true, true);
                        return;

                    default:
                        AnsiConsole.MarkupLine("[yellow]Dieser Status kann nicht überprüft werden.[/]");
                        return;
                }
            }**/
        }

        private void ShowProgress(Color fg, Color bg, int max, int min, int value, bool showMaxMin = false, bool showPercentage = false)
        {
            double percentage = (double)(value - min) / (max - min);

            var columns = new List<ProgressColumn>
            {
                new ProgressBarColumn
                {
                    CompletedStyle = new Style(fg),
                    RemainingStyle = new Style(bg),
                    Width = 40
                }
            };

            if (showMaxMin)
            {
                columns.Add(new TaskDescriptionColumn()
                {
                    Alignment = Justify.Center
                });
            }
            if (showPercentage)
            {
                columns.Add(new PercentageColumn()
                {
                    Style = Style.Plain,
                    CompletedStyle = Style.Plain,
                });
            }

            AnsiConsole.Progress()
                .AutoRefresh(false)
                .Columns(columns.ToArray())
                .Start(ctx =>
                {
                    var task = ctx.AddTask(value + "/" + max, new ProgressTaskSettings
                    {
                        MaxValue = 1
                    });

                    task.Increment(percentage);
                });
        }

    }
}
