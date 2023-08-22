using HxLocal;
using PatrickAssFucker.Entities;
using Spectre.Console;

namespace PatrickAssFucker.Areas
{
    public class Stillbach : Area
    {
        public override string Name => Localisation.GetString("areas.stillbach.name");
        public override string Description => Localisation.GetString("areas.stillbach.description");

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
            public override string Name => Localisation.GetString("areas.stillbach_road.name");
            public override string Description => Localisation.GetString("areas.stillbach_road.description");

            public Road() : base(AreaIdentifier.Stillbach_Road)
            {
            }
        }

        public class Blacksmith : Area
        {
            public override string Name => Localisation.GetString("areas.stillbach_blacksmith.name");
            public override string Description => Localisation.GetString("areas.stillbach_blacksmith.description");

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
                public override string Name => Localisation.GetString("areas.stillbach_blacksmith_groundfloor.name");
                public override string Description => Localisation.GetString("areas.stillbach_blacksmith_groundfloor.description");

                public GroundFloor() : base(AreaIdentifier.Stillbach_Blacksmith_GroundFloor)
                {
                    var finalDialogue = new Dialogue { NpcText = "wow oke danke", Options = new Option[0] };
                    var detailsDialogue = new Dialogue { NpcText = "ja habe ich. willst du details", Options = new[] { new Option { Name = "nein direkt annehmen", Action = () => { }, NextDialogue = finalDialogue } } };
                    var startDialogue = new Dialogue { NpcText = "Hi", Options = new[] { new Option { Name = "hast du eine quest", Action = () => { }, NextDialogue = detailsDialogue } } };

                    var npc = new DialogEntity(startDialogue);
                    var apple = new Item(ItemType.Apple);
                    
                    Add(npc);
                    Add(apple);
                }
            }

            public class FirstFloor : Area
            {
                public override string Name => Localisation.GetString("areas.stillbach_blacksmith_firstfloor.name");
                public override string Description => Localisation.GetString("areas.stillbach_blacksmith_firstfloor.description");

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
            public override string Name => Localisation.GetString("areas.stillbach_wellplace.name");
            public override string Description => Localisation.GetString("areas.stillbach_wellplace.description");

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