using System;

namespace PatrickAssFucker
{
    public abstract class Entity
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
    }
}
