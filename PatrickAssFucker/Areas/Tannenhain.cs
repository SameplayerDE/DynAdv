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
        public override string Name => Localisation.GetString("areas.tannenhain_forestpath.name");
        public override string Description => Localisation.GetString("areas.tannenhain_forestpath.description");
        
        public ForestPathRiver() : base(AreaIdentifier.Tannenhain_ForestPathRiver)
        {
            OnEnter = () =>
            {
                AnsiConsole.MarkupLine("Du den Weg entlang");
            };
        }
    }

    public class ForestPathBush : Area
    {
        
    }

}