using HxLocal;
using PatrickAssFucker.Entities;
using Spectre.Console;

namespace PatrickAssFucker.Areas
{
    public class Stillbach : Area
    {
        public override string Name => Localisation.GetString("areas.stillbach");
        public override string Description => Localisation.GetString("areas.stillbach_description");

        public Stillbach() : base(AreaIdentifier.Stillbach)
        {
            var road = new Road();
            var blacksmith = new Blacksmith();
            var wellPlace = new WellPlace();

            Add(road);
            Add(blacksmith);
            Add(wellPlace);
            
            Entrance = road;
            Area.Link(road, wellPlace);
            Area.Link(road, blacksmith);
            
            OnEnter = () =>
            {
                AnsiConsole.MarkupLine(Localisation.GetString("events.area_enter", Name));
            };
        }

        public class Road : Area
        {
            public override string Name => Localisation.GetString("areas.stillbach_road");
            public override string Description => Localisation.GetString("areas.stillbach_road_description");

            public Road() : base(AreaIdentifier.Stillbach_Road)
            {
            }
        }

        public class Blacksmith : Area
        {
            public override string Name => Localisation.GetString("areas.stillbach_blacksmith");
            public override string Description => Localisation.GetString("areas.stillbach_blacksmith_description");

            public Blacksmith() : base(AreaIdentifier.Stillbach_Blacksmith)
            {
                var groundFloor = new GroundFloor();
                var firstFloor = new FirstFloor();

                Add(groundFloor);
                Add(firstFloor);

                Entrance = groundFloor;
                Area.Link(groundFloor, firstFloor);
            }

            public class GroundFloor : Area
            {
                public override string Name => Localisation.GetString("areas.stillbach_blacksmith_groundfloor");
                public override string Description => Localisation.GetString("areas.stillbach_blacksmith_groundfloor_description");

                public GroundFloor() : base(AreaIdentifier.Stillbach_Blacksmith_GroundFloor)
                {
                    var blacksmith = new DialogEntity();
                    blacksmith.Name = "Schmied";

                    var apple = new Item(ItemType.Apple);
                    
                    Add(blacksmith);
                    Add(apple);

                    OnEnter = () =>
                    {
                        if (blacksmith.State == 1)
                        {
                            AnsiConsole.MarkupLine("[yellow]Du betritts die Schmiede. Der Schmied packt dich am Latz und wirft dich hinaus, auf die Schotterstraße, vor der Schmiede.[/]");
                            blacksmith.State = 2;
                            Brain.Instance.Player.MoveTo(AreaIdentifier.Stillbach_Road);
                        }

                        else if (blacksmith.State >= 2 && blacksmith.State < 4)
                        {
                            AnsiConsole.MarkupLine("[yellow]Du versucht die Tür zu öffnen doch sie ist verschlossen.[/]");
                            Brain.Instance.Player.MoveTo(AreaIdentifier.Stillbach_Road);
                            blacksmith.State++;
                        }
                        else if (blacksmith.State == 4)
                        {
                            AnsiConsole.MarkupLine("[yellow]Du versuchst die Tür zu öffnen; Ein lautes VERPISS DICH erhallt. Es entsprang dem Inneren der Schmiede.[/]");
                            Brain.Instance.Player.MoveTo(AreaIdentifier.Stillbach_Road);
                            blacksmith.State++;
                        }
                        else if (blacksmith.State > 4)
                        {
                            AnsiConsole.MarkupLine("[yellow]Du hast mit dem Feuer gespielt. Der Schmied stürmte aus seiner Schmiede und erschlug dich mit seinem Hammer.[/]");
                            AnsiConsole.Write(new FigletText("DU BIST GESTORBEN"));
                        }
                    };
                }
            }

            public class FirstFloor : Area
            {
                public override string Name => Localisation.GetString("areas.stillbach_blacksmith_firstfloor");
                public override string Description => Localisation.GetString("areas.stillbach_blacksmith_firstfloor_description");

                public FirstFloor() : base(AreaIdentifier.Stillbach_Blacksmith_FirstFloor)
                {
                    var bread = new Item(ItemType.Bread);
                    var breadMeta = bread.CreateMeta();
                    breadMeta.AddTag("expired", false);
                    bread.Meta = breadMeta;

                    var sword = new Item(ItemType.IronSword);
                    var swordMeta = sword.CreateMeta();
                    swordMeta.AddTag("translationKey", "items.midgetsword");
                    swordMeta.AddTag("damage", 15);
                    swordMeta.AddTag("durability", 10);
                    swordMeta.AddTag("weight", 12);
                    sword.Meta = swordMeta;
                    
                    Add(bread);
                    Add(sword);
                }
            }
        }

        public class WellPlace : Area
        {
            public override string Name => Localisation.GetString("areas.stillbach_wellplace");
            public override string Description => Localisation.GetString("areas.stillbach_wellplace_description");

            public WellPlace() : base(AreaIdentifier.Stillbach_WellPlace)
            {
                OnEnter = () =>
                {
                    AnsiConsole.MarkupLine("auf dem weg zum brunnenplatz hast du einen schlüssel gefunden");
                };
            }
        }
    }
}