using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Commands
{
    internal class HealCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Brain.Instance.Player.Heal();
            }
            else if (args.Length == 2)
            {
                if (args[0].Equals("add"))
                {
                    int expToAdd;
                    if (int.TryParse(args[1], out expToAdd))
                    {
                        Brain.Instance.Player.Health += expToAdd;
                    }
                    else
                    {
                        AnsiConsole.WriteLine($"'{args[1]}' is not a valid number.");
                    }
                }
                if (args[0].Equals("remove"))
                {
                    int expToAdd;
                    if (int.TryParse(args[1], out expToAdd))
                    {
                        Brain.Instance.Player.Health -= expToAdd;
                    }
                    else
                    {
                        AnsiConsole.WriteLine($"'{args[1]}' is not a valid number.");
                    }
                }
                if (args[0].Equals("set"))
                {
                    int expToAdd;
                    if (int.TryParse(args[1], out expToAdd))
                    {
                        Brain.Instance.Player.Health = expToAdd;
                    }
                    else
                    {
                        AnsiConsole.WriteLine($"'{args[1]}' is not a valid number.");
                    }
                }
            }
        }
    }
}
