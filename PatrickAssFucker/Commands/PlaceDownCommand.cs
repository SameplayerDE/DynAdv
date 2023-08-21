using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    public class PlaceDownCommand : ICommand
    {
        public void Execute(string[] args)
        {
            /**if (args.Length == 0)
            {
                if (Brain.Instance.Player.Equipped.Material != Material.NONE)
                {
                    AnsiConsole.MarkupLine("[yellow]Du legst[/] [gold1]" + Brain.Instance.Player.Equipped.Name + "[/] [yellow]ab.[/]");
                    //Brain.Instance.Room.AddItem(Brain.Instance.Player.Equipped);
                    Brain.Instance.Player.Equipped = new Item(Material.NONE, "nichts");
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]Du öffnest deine Hand.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Du kannst dies nicht tun.[/]");
            }**/
        }
    }
}
