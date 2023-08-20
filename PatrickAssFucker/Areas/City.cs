using HxLocal;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Areas
{
    public class City : Area
    {

        public override string Name => Localisation.GetString("areas.city");
        public override string Description => Localisation.GetString("areas.city_description");

        public City() : base(AreaIdentifier.City)
        {
            var market = new Marketplace();
            var townhall = new Townhall();

            Add(market);
            Add(townhall);

            Entrance = market;
            Area.Link(market, townhall);
        }

        public class Marketplace : Area
        {

            public override string Name => Localisation.GetString("areas.city_marketplace");
            public override string Description => Localisation.GetString("areas.city_marketplace_description");


            public Marketplace() : base(AreaIdentifier.City_Marketplace)
            {
                OnEnter = () =>
                {
                    AnsiConsole.MarkupLine(Localisation.GetString("events.area_with_parent_enter", Name, Parent!.Name));
                };
            }
        }

        public class Townhall : Area
        {

            public override string Name => Localisation.GetString("areas.city_townhall");
            public override string Description => Localisation.GetString("areas.city_townhall_description");


            public Townhall() : base(AreaIdentifier.City_Townhall)
            {
                var entrance = new Entrancehall();
                var dungeon = new Dungeon();
                
                Add(entrance);
                Add(dungeon);

                Area.Link(entrance, dungeon);
                Entrance = entrance;

                OnEnter = () =>
                {
                    AnsiConsole.MarkupLine(Localisation.GetString("events.area_with_parent_enter", Name, Parent!.Name));
                };
            }

            public class Entrancehall : Area
            {

                public override string Name => Localisation.GetString("areas.city_townhall_entrance");
                public override string Description => Localisation.GetString("areas.city_townhall_entrance_description");

                public Entrancehall() : base(AreaIdentifier.City_Townhall_Entrance)
                {
                }
            }

            public class Dungeon : Area
            {

                public override string Name => Localisation.GetString("areas.city_townhall_dungeon");
                public override string Description => Localisation.GetString("areas.city_townhall_dungeon_description");


                public Dungeon() : base(AreaIdentifier.City_Townhall_Dungeon)
                {
                    OnEnter = () =>
                    {
                        AnsiConsole.MarkupLine(Localisation.GetString("events.area_with_parent_enter", Name, Parent!.Name));
                    };
                }
            }

        }

    }
}
