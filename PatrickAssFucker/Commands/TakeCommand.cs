using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    public class TakeCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Du greifst in die Luft.[/]");
            }
            else if (args.Length == 1)
            {

            }
        }
    }
}
