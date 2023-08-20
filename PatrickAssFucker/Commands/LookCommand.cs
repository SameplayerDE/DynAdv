using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PatrickAssFucker.Commands
{
    public class LookCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                AnsiConsole.Status().Start("Du schaust dich um...", ctx =>
                {
                    ctx.Status("[yellow]Du schaust dich um...[/]");
                    ctx.Spinner(Spinner.Known.Line);
                    ctx.SpinnerStyle(Style.Parse("gold1"));
                    Thread.Sleep(2000);
                });


                AnsiConsole.MarkupLine(Brain.Instance.Player.CurrentArea.Description);

            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Du kannst dies nicht tun.[/]");
            }
        }
    }
}
