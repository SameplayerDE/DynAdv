using Spectre.Console;

namespace PatrickAssFucker.Entities;

public class DialogEntity : Entity , ITalkable
{

    public int State = 0;
    
    public void Talk()
    {
        AnsiConsole.MarkupLine("[gold1]Schmied[/] [white]>[/] [yellow]Verpiss dich![/]");
        Brain.Instance.Player.MoveTo(AreaIdentifier.Stillbach_Road);
        AnsiConsole.MarkupLine("[yellow]Der[/] [gold1]Schmied[/] [yellow]hat dich aus seiner Schmiede geworfen.[/]");
        State = 1;
    }
}