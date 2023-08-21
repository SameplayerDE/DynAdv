using PatrickAssFucker.Areas;

namespace PatrickAssFucker
{
    public enum LevelingType
    {
        Erratic,
        Fast,
        MediumFast,
        MediumSlow,
        Slow,
        Fluctuating
    }

    public class Player : Entity
    {
        public int Level = 1;
        public int Exp;
        public LevelingType LevelingCurve = LevelingType.Erratic;

        public QuestBook QuestBook { get; set; }
        public Area CurrentArea;

        public int Strength { get; internal set; }
        public int Defense { get; internal set; }
        public int Speed { get; internal set; }

        public Player() : base()
        {
            //Type = "player";
            //Description = "the player";
            //Name = "player";
            QuestBook = new QuestBook();
        }

        public void MoveTo(Area area)
        {
            if (area == null || CurrentArea == area)
            {
                return;
            }

            bool canMove = false;

            // Wenn CurrentArea null ist, kann der Spieler sich in jedem zugänglichen Bereich bewegen.
            if (CurrentArea == null && area.CanEnter())
            {
                canMove = true;
            }
            // Überprüfen Sie, ob die CurrentArea direkt mit dem Zielgebiet verbunden ist.
            else if (CurrentArea != null && CurrentArea.IsLinkedWith(area) && area.CanEnter())
            {
                canMove = true;
            }
            // Wenn sich der Spieler in einem Eingangsbereich befindet, überprüfen Sie, ob der übergeordnete Bereich mit dem Zielgebiet verbunden ist.
            else if (CurrentArea != null && CurrentArea.IsEntrance && CurrentArea.Parent != null && CurrentArea.Parent.IsLinkedWith(area) && area.CanEnter())
            {
                canMove = true;
            }

            if (canMove)
            {
                CurrentArea?.OnLeave?.Invoke();
                CurrentArea = area;
                CurrentArea.OnEnter?.Invoke();

                while (CurrentArea.HasEntrance) // Verwenden Sie eine Schleife anstelle einer einfachen Zuweisung.
                {
                    CurrentArea = CurrentArea.Entrance!;
                    CurrentArea.OnEnter?.Invoke();
                }
            }
        }


        public void MoveTo(AreaIdentifier id)
        {
            var area = AreaManager.Instance.GetAreaById(id);
            MoveTo(area);
        }

        /**public void AddExp(int exp)
        {
            Exp += exp;
            while (true)
            {
                int expNeeded = ExpNeededForLevel(Level + 1);
                if (Exp >= expNeeded)
                {
                    IncreaseLevel();
                    Exp -= expNeeded;
                }
                else
                {
                    break;
                }
            }
        }

        public void IncreaseLevel(int i = 1)
        {
            Level += i;
            Brain.Instance.QueueEvent(new PlayerLevelUpMessage() { Level = Level });
        }

        public int ExpNeededForLevel(int level)
        {
            switch (LevelingCurve)
            {
                case LevelingType.Erratic:
                    if (level <= 50)
                        return (int)(Math.Pow(level, 3) * (100 - level) / 50);
                    else if (level <= 68)
                        return (int)(Math.Pow(level, 3) * (150 - level) / 100);
                    else if (level <= 98)
                        return (int)(Math.Pow(level, 3) * ((1911 - 10 * level) / 3) / 500);
                    else
                        return (int)(Math.Pow(level, 3) * (160 - level) / 100);

                case LevelingType.Fast:
                    return (int)(4 * Math.Pow(level, 3) / 5);

                case LevelingType.MediumFast:
                    return (int)Math.Pow(Level, 3);

                case LevelingType.MediumSlow:
                    return (int)((6 / 5.0) * Math.Pow(level, 3) - 15 * Math.Pow(level, 2) + 100 * level - 140);

                case LevelingType.Slow:
                    return (int)(5 * Math.Pow(level, 3) / 4);

                case LevelingType.Fluctuating:
                    if (level <= 15)
                        return (int)(Math.Pow(level, 3) * ((level + 1) / 3 + 24) / 50);
                    else if (level <= 36)
                        return (int)(Math.Pow(level, 3) * (level + 14) / 50);
                    else
                        return (int)(Math.Pow(level, 3) * ((level / 2) + 32) / 50);

                default:
                    return 0; // Should never reach here
            }
        }**/
    }
}
