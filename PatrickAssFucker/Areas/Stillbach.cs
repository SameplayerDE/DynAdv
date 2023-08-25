using System.Collections;
using HxLocal;
using PatrickAssFucker.Audio;
using PatrickAssFucker.Entities;
using PatrickAssFucker.GameSystems;
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

                var startDialog = new Dialog();
                startDialog.Output = () => "Hey, was möchtest du von mir?";

                var child = new DialogEntity(startDialog);
                child.Name = "Kind";
                
                var askParents = new Dialog
                {
                    InputTitle = () => Localisation.GetString("dialogs.stillbach_road.child_of_blacksmith.ask_for_parent.input.title.default"),
                    InputText = () => Localisation.GetString("dialogs.stillbach_road.child_of_blacksmith.ask_for_parent.input.text.default"),
                    Output = () => "Mein Papa ist der Schmied von Stillbach. Ich habe ihn in der Schmiede eingesperrt und den Schlüssel verloren.",
                    IsAvailable = () => !StoryProgress.Instance.CheckCondition(ProgressType.Decisions, "parents"),
                    Action = () => { StoryProgress.Instance.SetCondition(ProgressType.Decisions, "parents", true); }
                };
                startDialog.Add(askParents);

                var whereIsKey = new Dialog()
                {
                    InputTitle = () => "Frage das Kind nach dem Schlüssel.",
                    InputText = () => "Wo denkst du hast du den Schlüssel verloren?",
                    Output = () => "Ich bin den Weg entlanggerannt und dann war er weg.",
                };
                askParents.Add(whereIsKey);
                
                var sayBye = new Dialog
                {
                    InputTitle = () => "Bye Bye!",
                    Output = () => "Tschüss!",
                    Action = () => {  }
                };
                startDialog.Add(sayBye);

                var kill = new Dialog
                {
                    InputTitle = () => "Töte das Kind mit deinen Fäusten",
                    InputText = () => "*DU SCHLÄGST GENÜSSLICH ZU*",
                    Output = () => "Was machst du mit mir aawaaaadaadaAacaafagfAAAAAAAaA. ....",
                    Action = () =>
                    {
                        Remove(child);
                        StoryProgress.Instance.SetCondition(ProgressType.Decisions, "kill", true);
                    }
                };
                startDialog.Add(kill);
                
                Add(child);
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

                    AudioInstance instance = new AudioInstance("Assets/door_heavy_shake.wav", false, 1f);
                    instance.Play();
                };
                OnEnter = () =>
                {
                    if (StoryProgress.Instance.CheckCondition(ProgressType.KeyEvents, "door_open"))
                    {
                        return;
                    }
                    else
                    {
                        StoryProgress.Instance.SetCondition(ProgressType.KeyEvents, "door_open", true);
                        if (StoryProgress.Instance.CheckCondition(ProgressType.KeyEvents, "try_open_door"))
                        {
                            AnsiConsole.MarkupLine("Vorsichtig steckst du den gefundenen Schlüssel ins Schloss...");
                            AnsiConsole.MarkupLine("Mit einem kleinen Ruck öffnet sich die Tür. Dein Herzschlag verlangsamt sich langsam, während du eintrittst.");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("Du nimmst den Schlüssel in die Hand und hältst den Atem an. Passt er wohl?");
                            AnsiConsole.MarkupLine("Du steckst den Schlüssel ins Schloss und drehst ihn langsam...");
                            AnsiConsole.MarkupLine("Klick! Die Tür öffnet sich und du trittst ein.");
                        }
                    }
                };


            }

            public class GroundFloor : Area
            {
                public override string Name => Localisation.GetString("areas.stillbach_blacksmith_groundfloor.name");
                public override string Description => Localisation.GetString("areas.stillbach_blacksmith_groundfloor.description");

                public GroundFloor() : base(AreaIdentifier.Stillbach_Blacksmith_GroundFloor)
                {
                    var dialog = new Dialog();
                    dialog.Output = () =>
                    {
                        if (StoryProgress.Instance.CheckCondition(ProgressType.Decisions, "kill"))
                        {
                            return "Hast du gerade diesen Schrei gehört. Es klang wie mein Kind.";
                        }
                        return "Mein Kind spielt vor der Schmiede.";
                    };
                    
                    var npc = new DialogEntity(dialog);
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