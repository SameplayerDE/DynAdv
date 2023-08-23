using System.Collections;
using HxLocal;
using PatrickAssFucker.Entities;
using PatrickAssFucker.Managers;
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
                StoryProgress.Instance.SetCondition(ProgressType.KeyEvents, "visit_stillbach", true);
                StoryProgress.Instance.SetCondition(ProgressType.KeyEvents, "visit_blacksmith", false);
                StoryProgress.Instance.SetCondition(ProgressType.KeyEvents, "visit_wellplace", false);
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
                
                CanEnter = () => StoryProgress.Instance.CheckCondition(ProgressType.KeyEvents, "visit_wellplace");
                OnEnterAttempt = () =>
                {
                    StoryProgress.Instance.SetCondition(ProgressType.KeyEvents, "try_open_door", true);
                    AnsiConsole.MarkupLine("Die Tür ist verschlossen.");
                };
                OnEnter = () =>
                {
                    if (StoryProgress.Instance.CheckCondition(ProgressType.KeyEvents, "try_open_door"))
                    {
                        AnsiConsole.MarkupLine("Vorsichtig steckst du den gefundenen Schlüssel ins Schloss...");
                        Thread.Sleep(4000); // Eine kurze Pause, um die Unsicherheit zu betonen

                        AnsiConsole.MarkupLine("Mit einem kleinen Ruck öffnet sich die Tür. Dein Herzschlag verlangsamt sich langsam, während du eintrittst.");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("Du nimmst den Schlüssel in die Hand und hältst den Atem an. Passt er wohl?");
                        Thread.Sleep(4000); // Eine Pause für die Unsicherheit

                        AnsiConsole.MarkupLine("Du steckst den Schlüssel ins Schloss und drehst ihn langsam...");
                        Thread.Sleep(2000); // Eine längere Pause für Spannung

                        AnsiConsole.MarkupLine("Klick! Die Tür öffnet sich und du trittst ein.");
                    }
                };


            }

            public class GroundFloor : Area
            {
                public override string Name => Localisation.GetString("areas.stillbach_blacksmith_groundfloor.name");
                public override string Description => Localisation.GetString("areas.stillbach_blacksmith_groundfloor.description");

                public GroundFloor() : base(AreaIdentifier.Stillbach_Blacksmith_GroundFloor)
                {
                    var aboutTownDialogue = new Dialogue();
                    var recentEventsDialogue = new Dialogue();
                    var aboutHimDialogue = new Dialogue();

                    aboutHimDialogue.NpcText = "Über mich? Nun, ich bin Schmied, wie du siehst. Mein Vater war's, sein Vater auch. Arbeit ist hart, aber ehrlich. Was willst du noch wissen, hm?";
                    aboutHimDialogue.Options = new[]
                    {
    new Option { Name = "Erzähl mir mehr über Stillbach.", Action = () => { }, NextDialogue = aboutTownDialogue },
    new Option { Name = "Was ist hier kürzlich passiert?", Action = () => { }, NextDialogue = recentEventsDialogue },
    new Option { Name = "Tschüss.", Action = () => { Console.WriteLine("Mach's gut, Fremder."); }, NextDialogue = null }
};

                    recentEventsDialogue.NpcText = "Was neues? 'n Sturm hat letztens 'n Baum umgehauen. Ansonsten? Geschäft wie immer. Nich' viel los hier. Warum fragst du?";
                    recentEventsDialogue.Options = new[]
                    {
    new Option { Name = "Erzähl mir mehr über dich.", Action = () => { }, NextDialogue = aboutHimDialogue },
    new Option { Name = "Tschüss.", Action = () => { Console.WriteLine("Mach's gut, Fremder."); }, NextDialogue = null }
};

                    aboutTownDialogue.NpcText = "Stillbach, hm? Nun, 'n ruhiger Ort, das ist er. Viel passiert hier nich'. Du willst Spaß, musst woanders hingehen. Aber die Leut' hier sind ehrlich und das Land ist schön. Jetzt lass mich arbeiten!";
                    aboutTownDialogue.Options = new[]
                    {
    new Option { Name = "Erzähl mir mehr über dich.", Action = () => { }, NextDialogue = aboutHimDialogue },
    new Option { Name = "Tschüss.", Action = () => { Console.WriteLine("Mach's gut, Fremder."); }, NextDialogue = null }
};

                    var startDialogue = new Dialogue
                    {
                        NpcText = "Hm? Was willst du? Bin beschäftigt. Schnell, raus damit!",
                        Options = new[]
                        {
        new Option { Name = "Erzähl mir mehr über dich.", Action = () => { }, NextDialogue = aboutHimDialogue },
        new Option { Name = "Erzähl mir mehr über Stillbach.", Action = () => { }, NextDialogue = aboutTownDialogue },
        new Option { Name = "Was ist hier kürzlich passiert?", Action = () => { }, NextDialogue = recentEventsDialogue },
        new Option { Name = "Tschüss.", Action = () => { Console.WriteLine("Mach's gut, Fremder."); }, NextDialogue = null }
    }
                    };

                    var npc = new DialogEntity(startDialogue);
                    npc.Name = "Schmied";
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
                    StoryProgress.Instance.SetCondition(ProgressType.KeyEvents, "visit_wellplace", true);

                    AnsiConsole.MarkupLine("Auf dem Weg zum Brunnenplatz hast du einen Schlüssel gefunden.");
                    if (!StoryProgress.Instance.CheckCondition(ProgressType.KeyEvents, "try_open_door"))
                    {
                        AnsiConsole.MarkupLine("Du fragst dich welches Schloss diese Schlüssel wohl öffnen mag.");
                        AnsiConsole.MarkupLine("Du steckst ihn ein, er könnte nützlich sein.");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("Er hat ähnliche verzierrungen wie das Türschloss der Schmiede.");
                        AnsiConsole.MarkupLine("Ob er dort wohl passt?");
                    }
                };
            }
        }
    }
}