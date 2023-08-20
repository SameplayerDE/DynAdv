namespace PatrickAssFucker
{
    public class PlayerLevelUpMessage : GameMessage
    {

        public int Level;

        public int HealthIncrease { get; set; }
        public int StrengthIncrease { get; set; }
        public int DefenseIncrease { get; set; }
        public int SpeedIncrease { get; set; }

    }
}