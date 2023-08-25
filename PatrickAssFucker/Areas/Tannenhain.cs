using HxLocal;
using Spectre.Console;

namespace PatrickAssFucker.Areas;

public class Tannenhain : Area
{

    public override string Name => Localisation.GetString("areas.tannenhain.name");
    public override string Description => Localisation.GetString("areas.tannenhain.description");
    
    public Tannenhain() : base(AreaIdentifier.Tannenhain)
    {
        Add(new ForestPathRiver());
        OnEnter = () =>
        {
            AnsiConsole.MarkupLine("Du hast den Tannenhain betreten.");
        };
    }
    
    public class ForestPathRiver : Area
    {
        public override string Name
        {
            get
            {
                if (Brain.Instance.Player.CurrentArea.Id == AreaIdentifier.Tannenhain)
                {
                    
                    return Localisation.GetString("areas.tannenhain_forestpath.out.name");
                }
                else
                {
                    
                    return Localisation.GetString("areas.tannenhain_forestpath.in.name");
                }
            }
        }

        public override string Description => Localisation.GetString("areas.tannenhain_forestpath.description");
        
        public ForestPathRiver() : base(AreaIdentifier.Tannenhain_ForestPathRiver)
        {
            OnEnter = () =>
            {
                AnsiConsole.MarkupLine("Du gehst den Weg entlang");
                var random = Brain.Instance.Random.Next(0, 100);
                if (random > 99)
                {
                    AnsiConsole.MarkupLine("[reverse]Du wurdest auf deiner Reise angegriffen, vergewaltigt und ermordet.[/]");
                }
                else if (random > 90)
                {
                    AnsiConsole.MarkupLine("[reverse]Du wurdest angegriffen.[/]");
                }
                else if (random > 80)
                {
                    AnsiConsole.MarkupLine("[reverse]Ein alter Mann am Wegesrand hat dir auf den Arsch gehauen.[/]");
                }
                else if (random > 70)
                {
                    AnsiConsole.MarkupLine("[reverse]Du hast den Vögeln beim singen gelauscht.[/]");
                }
            };
        }
    }

    public class ForestPathBush : Area
    {
        
    }

}