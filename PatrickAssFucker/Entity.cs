namespace PatrickAssFucker
{
    public class Entity
    {
        private Area _currentArea;
        private string _name;
        
        public Area CurrentArea
        {
            get
            {
                return _currentArea;
            }
            protected set
            {
                _currentArea = value;
            }
        }
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
    /*public abstract class Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public int MaxHealth { get; set; } = 100;

        private int _health = 100;
        public int Health
        {
            get => _health;
            set
            {
                _health = Math.Clamp(value, 0, MaxHealth);

                if (_health <= 0)
                {
                    OnDeath?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler OnDeath;

        public void Heal(int i = 0)
        {
            if (i == 0)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += i;
            }
        }
    }*/
}
