using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HxLocal;
using PatrickAssFucker.Entities;

namespace PatrickAssFucker.Commands
{
    public class LookCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {

                var area = Brain.Instance.Player.CurrentArea;
                
                //AnsiConsole.Status().Start(Localisation.GetString("commands.look_looking"), ctx =>
                //{
                //    //ctx.Status();
                //    //ctx.Spinner(Spinner.Known.Line);
                //    Thread.Sleep(2000);
                //});

                AnsiConsole.MarkupLine(area.Description);
                if (area.HasItems)
                {
                    if (area.Items.Count == 1)
                    {
                        AnsiConsole.MarkupLine(Localisation.GetString("commands.look_found_item"));
                    }
                    else
                    {
                        AnsiConsole.MarkupLine(Localisation.GetString("commands.look_found_items"));
                    }

                    foreach (var item in area.Items)
                    {
                        if (item.HasMeta)
                        {
                            var meta = item.Meta;
                            if (meta.Displayname != null)
                            {
                                AnsiConsole.MarkupLine("- " + meta.Displayname);
                                continue;
                            }
                            if (meta.HasTag("translationKey"))
                            {
                                var key = meta.GetTag<string>("translationKey");
                                AnsiConsole.MarkupLine("- " + Localisation.GetString(key));
                                continue;
                            }
                        }
                        AnsiConsole.MarkupLine("- " + item.Name);
                    }
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Du kannst dies nicht tun.[/]");
            }
        }
    }
}
