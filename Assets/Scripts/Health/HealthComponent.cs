using System;

namespace collegeGame.Health
{
    [Serializable]
    public class HealthComponent : IHealth
    {
        private float _maxHealth;
        private float _health;
        public event Action HealthChanged;
        public event Action Died;

        public HealthComponent(float health)
        {
            _maxHealth = health;
            _health = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            HealthChanged?.Invoke();
            if (_health < damage)
            {
                Died?.Invoke();
            }

            _health -= damage;
        }

        public float GetHealth() => _health;
    }
}
